using BookShop.Application.Models;
using BookShop.Infrastructure;
using BookShop.Infrastructure.DataModels;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Application;

public class BookService : IBookService
{
    private readonly ApplicationDbContext _db;

    public BookService(ApplicationDbContext db)
    {
        _db = db;
    }

    public void Create(BookCreateModel input)
    {
        _db.Books.Add(input.Adapt<BookData>());

        _db.SaveChanges();
    }

    public BookDetails GetDetails(int bookId)
    {
        var book = _db.Books.Include(b => b.Ratings)
            .ProjectToType<BookDetails>()
            .First(b => b.Id == bookId);
        return book;
    }

    public IList<BookItem> GetAll(string term = "")
    {
        var q = _db.Books;

        if (string.IsNullOrEmpty(term))
        {
            return q.Include(b => b.Category)
            .ProjectToType<BookItem>().ToList();
        }

        return q.Where(b => b.Name.StartsWith(term) || (b.AuthorDetails != null && b.AuthorDetails.Name.StartsWith(term)))
            .Include(b => b.Category)
            .ProjectToType<BookItem>().ToList();
    }

    public BookEditModel GetEdit(int bookId)
    {
        var book = _db.Books.Find(bookId);

        var result = book.Adapt<BookEditModel>();
        result.AuthorName = book.AuthorDetails.Name;
        result.AuthorEmail = book.AuthorDetails.Email;

        return result;
    }

    public void Update(BookEditModel input)
    {
        var book = _db.Books.Find(input.Id);

        book.Name = input.Name;
        book.Language = input.Language;
        book.CategoryId = input.CategoryId;
        book.FileName = input.FileName;

        var author = new Author { Email = input.AuthorEmail, Name = input.AuthorName };

        book.AuthorDetails = author;

        if (input.CoverImage is not null)
        {
            book.CoverImage = input.CoverImage;
        }

        _db.SaveChanges();
    }

    public ICollection<BookCategory> GetAllCategories()
    {
        return _db.Categories.ToList();
    }
}
