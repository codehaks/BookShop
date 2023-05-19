using Ardalis.GuardClauses;
using BookShop.Application.Interfaces;
using BookShop.Application.Models;
using BookShop.Infrastructure;
using BookShop.Infrastructure.DataModels;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Application.Services;

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

        return q.Where(b => b.Name.StartsWith(term)).Include(b => b.Category)
            .ProjectToType<BookItem>().ToList();
    }

    public BookEditModel GetEdit(int bookId)
    {
        var book = _db.Books.Find(bookId);
        Guard.Against.Null(book);
        return book.Adapt<BookEditModel>();
    }

    public void Update(BookEditModel input)
    {
        var book = _db.Books.Find(input.Id);
        Guard.Against.Null(book);
        book.Name = input.Name;
        book.Language = input.Language;
        book.CategoryId = input.CategoryId;
        book.FileName = input.FileName;

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
