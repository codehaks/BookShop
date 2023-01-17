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
        });

        _db.SaveChanges();
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
