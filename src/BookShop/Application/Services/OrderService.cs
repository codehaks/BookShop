using Ardalis.GuardClauses;
using BookShop.Application.Interfaces;
using BookShop.Application.Models;
using BookShop.Infrastructure;
using BookShop.Infrastructure.DataModels;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookShop.Application.Services;

/// <summary>
/// Service for managing order operations including creation, confirmation, and rating.
/// </summary>
public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<OrderService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderService"/> class.
    /// </summary>
    /// <param name="db">The application database context.</param>
    /// <param name="logger">The logger instance.</param>
    public OrderService(ApplicationDbContext db, ILogger<OrderService> logger)
    {
        _db = db;
        _logger = logger;
    }

    /// <summary>
    /// Gets detailed information about a specific order.
    /// </summary>
    /// <param name="orderId">The ID of the order to retrieve.</param>
    /// <returns>Detailed information about the order.</returns>
    /// <exception cref="InvalidOperationException">Thrown when order is not found.</exception>
    public OrderDetails Get(int orderId)
    {
        _logger.LogDebug("Retrieving order details for order ID: {OrderId}", orderId);
        
        var order = _db.Orders
            .Include(o => o.User)
            .Include(o => o.Book)
            .ThenInclude(b => b.Category)
            .First(o => o.Id == orderId);

        return order.Adapt<OrderDetails>();
    }

    /// <summary>
    /// Gets all orders in the system.
    /// </summary>
    /// <returns>A list of all orders.</returns>
    public IList<OrderItem> GetAll()
    {
        _logger.LogDebug("Retrieving all orders");
        
        var orderList = _db.Orders
            .Include(o => o.User)
            .Include(o => o.Book)
            .AsNoTracking()
            .ProjectToType<OrderItem>()
            .ToList();

        return orderList;
    }

    /// <summary>
    /// Gets all confirmed orders for a specific user.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>A list of user's confirmed orders.</returns>
    public IList<UserOrderItem> GetAllByUser(string userId)
    {
        Guard.Against.NullOrEmpty(userId, nameof(userId));
        
        _logger.LogDebug("Retrieving orders for user: {UserId}", userId);
        
        var orderList = _db.Orders
            .Include(o => o.User)
            .Include(o => o.Book)
            .Include(o => o.Rating)
            .Where(o => o.UserId == userId && o.State == OrderState.Confirmed)
            .ProjectToType<UserOrderItem>()
            .ToList();

        return orderList;
    }

    /// <summary>
    /// Adds a rating to an order.
    /// </summary>
    /// <param name="orderId">The ID of the order to rate.</param>
    /// <param name="score">The rating score (1-5).</param>
    /// <exception cref="NotFoundException">Thrown when order is not found.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when score is not between 1 and 5.</exception>
    public void AddRating(int orderId, int score)
    {
        Guard.Against.OutOfRange(score, nameof(score), 1, 5);
        
        _logger.LogInformation("Adding rating {Score} to order ID: {OrderId}", score, orderId);
        
        var order = _db.Orders.Find(orderId);
        Guard.Against.Null(order, nameof(order));
        
        order.Rating = new RatingData
        {
            BookId = order.BookId,
            OrderId = orderId,
            TimeCreated = DateTime.UtcNow,
            Score = (RatingScore)score
        };

        _db.SaveChanges();
        
        _logger.LogInformation("Rating added successfully to order ID: {OrderId}", orderId);
    }

    /// <summary>
    /// Creates a new order.
    /// </summary>
    /// <param name="model">The order creation model.</param>
    /// <returns>The ID of the newly created order.</returns>
    /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
    /// <exception cref="NotFoundException">Thrown when book is not found.</exception>
    public int Create(OrderCreateModel model)
    {
        Guard.Against.Null(model, nameof(model));
        
        _logger.LogInformation("Creating new order for book ID: {BookId}, user: {UserId}", 
            model.BookId, model.UserId);
        
        var order = model.Adapt<OrderData>();
        var book = _db.Books.Find(model.BookId);
        Guard.Against.Null(book, nameof(book));

        order.Details = new OrderDataDetails { Book = book.Adapt<BookInfo>() };
        order.TimeCreated = DateTime.UtcNow;
        order.State = OrderState.New;

        _db.Orders.Add(order);
        _db.SaveChanges();
        
        _logger.LogInformation("Order created successfully with ID: {OrderId}", order.Id);

        return order.Id;
    }

    /// <summary>
    /// Confirms an order, changing its state from New to Confirmed.
    /// </summary>
    /// <param name="orderId">The ID of the order to confirm.</param>
    /// <exception cref="NotFoundException">Thrown when order is not found.</exception>
    public void Confirm(int orderId)
    {
        _logger.LogInformation("Confirming order ID: {OrderId}", orderId);
        
        var order = _db.Orders.Find(orderId);
        Guard.Against.Null(order, nameof(order));

        if (order.State == OrderState.New)
        {
            order.State = OrderState.Confirmed;
            _db.SaveChanges();
            _logger.LogInformation("Order confirmed successfully: {OrderId}", orderId);
        }
        else
        {
            _logger.LogWarning("Order {OrderId} is not in New state, current state: {State}", 
                orderId, order.State);
        }
    }

    /// <summary>
    /// Gets a user's confirmed order for a specific book.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="bookId">The ID of the book.</param>
    /// <returns>Order details if found, null otherwise.</returns>
    public OrderDetails GetUserBook(string userId, int bookId)
    {
        Guard.Against.NullOrEmpty(userId, nameof(userId));
        
        _logger.LogDebug("Retrieving order for user: {UserId}, book: {BookId}", userId, bookId);
        
        var order = _db.Orders
           .Include(o => o.User)
           .Include(o => o.Book)
           .ThenInclude(b => b.Category)
           .FirstOrDefault(o => o.UserId == userId 
               && o.BookId == bookId 
               && o.State == OrderState.Confirmed);

        return order.Adapt<OrderDetails>();
    }
}
