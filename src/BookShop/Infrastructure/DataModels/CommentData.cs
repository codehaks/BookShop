namespace BookShop.Infrastructure.DataModels;

public class CommentData
{
    public int Id { get; set; }
    public required string UserId { get; set; }
    public required string UserName { get; set; }
    public required string Note { get; set; }

    public int BookId { get; set; }

    public DateTime TimeCreated { get; set; }
}
