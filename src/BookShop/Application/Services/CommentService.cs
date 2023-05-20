using BookShop.Application.Interfaces;
using BookShop.Infrastructure;
using BookShop.Infrastructure.DataModels;
using Mapster;

namespace BookShop.Application.Services;

public class CommentService : ICommentService
{
    private readonly ApplicationDbContext _db;

    public CommentService(ApplicationDbContext db)
    {
        _db = db;
    }

    public void Create(string userId, string userName, int bookId, string note)
    {
        _db.Comments.Add(new CommentData
        {
            UserId = userId,
            UserName = userName,
            Note = note,
            BookId = bookId,
            TimeCreated = DateTime.UtcNow
        });

        _db.SaveChanges();
    }

    public IList<CommentOutput> GetAllByBook(int bookId)
    {
        return _db.Comments
            .OrderByDescending(c => c.TimeCreated)
            .ProjectToType<CommentOutput>().ToList();
    }
}

public class CommentOutput
{
    public int Id { get; set; }
    public required string UserId { get; set; }
    public required string UserName { get; set; }
    public required string Note { get; set; }

    public int BookId { get; set; }

    public DateTime TimeCreated { get; set; }
}
