namespace BookShop.Application
{
    public interface ICommentService
    {
        void Create(string userId, string userName, int bookId, string note);

        IList<CommentOutput> GetAllByBook(int bookId);
    }
}
