using System.ComponentModel.DataAnnotations;

namespace BookShop.Infrastructure.DataModels;

public class OrderDataDetails
{
    public required BookInfo Book { get; set; }
}

public class BookInfo
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public LanguageType Language { get; set; }
    public required string Description { get; set; }
    public int Price { get; set; }
    public required string Author { get; set; }
}
public class OrderData
{

    public OrderDataDetails? Details { get; set; }
    public int Id { get; set; }

    public RatingData Rating { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public int BookId { get; set; }
    public BookData Book { get; set; }

    public int Amount { get; set; }

    public OrderState State { get; set; }

    public DateTime TimeCreated { get; set; }
}

public enum OrderState
{
    New = 0,
    Confirmed = 1,
    Canceled = 2
}
