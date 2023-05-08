namespace BookShop.Infrastructure.DataModels;

public class CommentData
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string Note { get; set; }

    public int BookId { get; set; }

    public DateTime TimeCreated { get; set; }
}
