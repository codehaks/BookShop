using BookShop.Application.Models;
using BookShop.Infrastructure.DataModels;

namespace BookShop.Application.Interfaces;

public interface IBookService
{
    IList<BookItem> GetAll(string term = "");

    void Create(BookCreateModel input);

    BookDetails GetDetails(int bookId);

    BookEditModel GetEdit(int bookId);

    void Update(BookEditModel input);

    ICollection<BookCategory> GetAllCategories();
}
