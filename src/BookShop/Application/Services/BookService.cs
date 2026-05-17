using Ardalis.GuardClauses;
using BookShop.Application.Interfaces;
using BookShop.Application.Mappers;
using BookShop.Application.Models;
using BookShop.Domain;
using BookShop.Infrastructure;
using BookShop.Infrastructure.DataModels;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookShop.Application.Services;

/// <summary>
/// Service for managing book operations including CRUD operations and category management.
/// </summary>
public class BookService : IBookService
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<BookService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="BookService"/> class.
    /// </summary>
    /// <param name="db">The application database context.</param>
    /// <param name="logger">The logger instance.</param>
    public BookService(ApplicationDbContext db, ILogger<BookService> logger)
    {
        _db = db;
        _logger = logger;
    }

    /// <summary>
    /// Creates a new book in the database.
    /// </summary>
    /// <param name="input">The book creation model containing book details.</param>
    /// <exception cref="ArgumentNullException">Thrown when input is null.</exception>
    public void Create(BookCreateModel input)
    {
        Guard.Against.Null(input, nameof(input));
        
        _logger.LogInformation("Creating new book: {BookName}", input.Name);
        
        var year = new Year(input.Year);

        var book = input.Adapt<BookData>();
        book.Year = year.Value;

        _db.Books.Add(book);
        _db.SaveChanges();
        
        _logger.LogInformation("Book created successfully with ID: {BookId}", book.Id);
    }

    /// <summary>
    /// Gets detailed information about a specific book including ratings.
    /// </summary>
    /// <param name="bookId">The ID of the book to retrieve.</param>
    /// <returns>Detailed information about the book.</returns>
    /// <exception cref="InvalidOperationException">Thrown when book is not found.</exception>
    public BookDetails GetDetails(int bookId)
    {
        _logger.LogDebug("Retrieving details for book ID: {BookId}", bookId);
        
        var book = _db.Books.Include(b => b.Ratings)
            .First(b => b.Id == bookId);
            
        return BookMappers.MapToBookDetails(book);
    }

    /// <summary>
    /// Gets all books or filters books by search term.
    /// </summary>
    /// <param name="term">Optional search term to filter books by name.</param>
    /// <returns>A list of book items.</returns>
    public IList<BookItem> GetAll(string term = "")
    {
        _logger.LogDebug("Retrieving all books with search term: {SearchTerm}", term ?? "none");
        
        var q = _db.Books;

        if (string.IsNullOrEmpty(term))
        {
            return q.Include(b => b.Category)
                .ProjectToType<BookItem>()
                .ToList();
        }

        return q.Where(b => b.Name.StartsWith(term))
            .Include(b => b.Category)
            .ProjectToType<BookItem>()
            .ToList();
    }

    /// <summary>
    /// Gets book information for editing.
    /// </summary>
    /// <param name="bookId">The ID of the book to edit.</param>
    /// <returns>Book edit model with current book data.</returns>
    /// <exception cref="NotFoundException">Thrown when book is not found.</exception>
    public BookEditModel GetEdit(int bookId)
    {
        _logger.LogDebug("Retrieving book for editing: {BookId}", bookId);
        
        var book = _db.Books.Find(bookId);
        Guard.Against.Null(book, nameof(book));
        
        return book.Adapt<BookEditModel>();
    }

    /// <summary>
    /// Updates an existing book with new information.
    /// </summary>
    /// <param name="input">The book edit model containing updated information.</param>
    /// <exception cref="NotFoundException">Thrown when book is not found.</exception>
    /// <exception cref="ArgumentNullException">Thrown when input is null.</exception>
    public void Update(BookEditModel input)
    {
        Guard.Against.Null(input, nameof(input));
        
        _logger.LogInformation("Updating book ID: {BookId}", input.Id);
        
        var book = _db.Books.Find(input.Id);
        Guard.Against.Null(book, nameof(book));
        
        book.Name = input.Name;
        book.Language = input.Language;
        book.CategoryId = input.CategoryId;
        book.FileName = input.FileName;

        if (input.CoverImage is not null)
        {
            _logger.LogDebug("Updating cover image for book ID: {BookId}", input.Id);
            book.CoverImage = input.CoverImage;
        }

        _db.SaveChanges();
        
        _logger.LogInformation("Book updated successfully: {BookId}", input.Id);
    }

    /// <summary>
    /// Gets all available book categories.
    /// </summary>
    /// <returns>A collection of all book categories.</returns>
    public ICollection<BookCategory> GetAllCategories()
    {
        _logger.LogDebug("Retrieving all book categories");
        
        return _db.Categories.ToList();
    }
}
