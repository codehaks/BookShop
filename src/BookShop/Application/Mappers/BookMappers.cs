using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShop.Application.Models;
using BookShop.Infrastructure.DataModels;

namespace BookShop.Application.Mappers;
public static class BookMappers
{
    public static BookDetails MapToBookDetails(BookData bookData)
    {
        var bookDetails = new BookDetails
        {
            Id = bookData.Id,
            Ratings = bookData.Ratings,
            Name = bookData.Name,
            FileName = bookData.FileName,
            Language = Enum.GetName(typeof(LanguageType), bookData.Language),
            CoverImage = bookData.CoverImage,
            Description = bookData.Description,
            Price = bookData.Price,
            Author = bookData.Author,
            Year = bookData.Year,
            Pages = bookData.Pages
        };

        return bookDetails;
    }
}
