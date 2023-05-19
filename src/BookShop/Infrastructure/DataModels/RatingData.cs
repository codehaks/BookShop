namespace BookShop.Infrastructure.DataModels;

public enum RatingScore
{
    One = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
}

public class RatingData
{
    public int BookId { get; set; }
    public BookData Book { get; set; } = default!;
    public int OrderId { get; set; }
    public OrderData Order { get; set; } = default!;

    public RatingScore Score { get; set; }

    public DateTime TimeCreated { get; set; }
}
