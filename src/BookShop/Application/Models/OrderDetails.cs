using BookShop.Infrastructure.DataModels;

namespace BookShop.Application.Models;

public class OrderDetails
{
    public int Id { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public int BookId { get; set; }
    public BookData Book { get; set; }

    public int Amount { get; set; }

    public OrderState State { get; set; }

    public DateTime TimeCreated { get; set; }
}

public class OrderItem
{
    public int Id { get; set; }

    public OrderDataDetails? Details { get; set; }

    public string UserId { get; set; }
    public string UserUserName { get; set; }

    public int BookId { get; set; }
    public string BookName { get; set; }

    public int Amount { get; set; }

    public OrderState State { get; set; }

    public DateTime TimeCreated { get; set; }
}

public class UserOrderItem
{
    public int Id { get; set; }

    public int? RatingScore { get; set; }

    public string UserId { get; set; }

    public int BookId { get; set; }
    public string BookName { get; set; }

    public int Amount { get; set; }
    public DateTime TimeCreated { get; set; }
}
