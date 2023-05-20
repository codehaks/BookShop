namespace BookShop.Application.Models;

public class OrderCreateModel
{
    public required string UserId { get; set; }
    public int BookId { get; set; }
    public int Amount { get; set; }
}
