using BookShop.Application.Models;
using BookShop.Infrastructure;
using BookShop.Infrastructure.DataModels;
using Elfie.Serialization;
using Mapster;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
        var book=_db.Books.Find(bookId);
        return book.Adapt<BookDetails>();
    }

    public IList<BookItem> GetAll()
    {
        return _db.Books.Include(b=>b.Category)
            .ProjectToType<BookItem>().ToList();
    }

    public BookEditModel GetEdit(int bookId)
    {
        var book = _db.Books.Find(bookId);
        return book.Adapt<BookEditModel>();
    }

    public void Update(BookEditModel input)
    {
        var book = _db.Books.Find(input.Id);

        book.Name=input.Name;
        book.Language=input.Language;
        book.CategoryId=input.CategoryId;

        _db.SaveChanges();

    }

    public ICollection<BookCategory> GetAllCategories()
    {
        return _db.Categories.ToList();
    }
}
