namespace BookShop.Infrastructure.DataModels;

public class RatingData
{
    public int BookId { get; set; }
    public BookData Book { get; set; } = default!;
    public int OrderId { get; set; }
    public OrderData Order { get; set; } = default!;

    public RatingScore Score { get; set; }

    public DateTime TimeCreated { get; set; }
}
