using BookShop.Infrastructure.DataModels;

namespace BookShop.Application.Models;

public class OrderDetails
{
    public int Id { get; set; }

    public required string UserId { get; set; }
    public ApplicationUser? User { get; set; }

    public int BookId { get; set; }
    public BookData? Book { get; set; }

    public int Amount { get; set; }

    public OrderState State { get; set; }

    public DateTime TimeCreated { get; set; }
}

public class OrderItem
{
    public int Id { get; set; }

    public required string UserId { get; set; }
    public required string UserUserName { get; set; }

    public int BookId { get; set; }
    public required string BookName { get; set; }

    public int Amount { get; set; }

    public OrderState State { get; set; }

    public DateTime TimeCreated { get; set; }
}

public class UserOrderItem
{
    public int Id { get; set; }

    public int? RatingScore { get; set; }

    public required string UserId { get; set; }

    public int BookId { get; set; }
    public required string BookName { get; set; }

    public int Amount { get; set; }
    public DateTime TimeCreated { get; set; }
}
