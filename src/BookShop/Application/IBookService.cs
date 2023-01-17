using BookShop.Application.Models;
using BookShop.Infrastructure.DataModels;

namespace BookShop.Application
{
    public interface IBookService
    {
        IList<BookItem> GetAll();
        void Create(BookCreateModel input);
        BookDetails GetDetails(int bookId);
    }
}