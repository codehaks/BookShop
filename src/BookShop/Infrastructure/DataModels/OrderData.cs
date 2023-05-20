using BookShop.Infrastructure.Enums;

namespace BookShop.Infrastructure.DataModels;

public class OrderData
{
    public int Id { get; set; }

    public RatingData? Rating { get; set; }

    public required string UserId { get; set; }
    public ApplicationUser? User { get; set; }

    public int BookId { get; set; }
    public BookData? Book { get; set; }

    public int Amount { get; set; }

    public OrderState State { get; set; }

    public DateTime TimeCreated { get; set; }
}
