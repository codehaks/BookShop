using BookShop.Application.Models;
using BookShop.Infrastructure;
using BookShop.Infrastructure.DataModels;
using Elfie.Serialization;
using Mapster;
using Microsoft.CodeAnalysis.FlowAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
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
        return _db.Books.ProjectToType<BookItem>().ToList();
    }

}
