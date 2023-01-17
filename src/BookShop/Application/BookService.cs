using BookShop.Application.Models;
using BookShop.Infrastructure;
using BookShop.Infrastructure.DataModels;
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
        _db.Books.Add(new BookData
        {
            Author = input.Author,
            Description = input.Description,
            Name = input.Name,
            Pages = input.Pages,
            Price = input.Price,
            Year = input.Year,
            CoverImage= input.CoverImage,
        });

        _db.SaveChanges();
    }

    public BookDetails GetDetails(int bookId)
    {
        var book=_db.Books.Find(bookId);
        var result = new BookDetails
        {
            Author = book.Author,
            CoverImage = book.CoverImage,
            Name = book.Name,
            Description = book.Description,
            Id = book.Id,
            Pages = book.Pages,
            Price = book.Price,
            Year = book.Year
        };

        return result;
    }

    public IList<BookItem> GetAll()
    {
        return _db.Books.Select(b => new BookItem
        {
            Id = b.Id,
            Name = b.Name,
            Price = b.Price,
        }).ToList();
    }

}
