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
            Language = bookData.Language.ToString(),
            CoverImage = bookData.CoverImage,
            Description = bookData.Description,
            Price = bookData.Price,
            Author = bookData.Author,
            Year = bookData.Year,
            Pages = bookData.Pages
        };

        return bookDetails;
    }

    public static BookEditModel MapToBookEdit(BookData bookData)
    {
        var bookDetails = new BookEditModel
        {
            Id = bookData.Id,

            Name = bookData.Name,
            FileName = bookData.FileName,
            Language = bookData.Language,
            CoverImage = bookData.CoverImage,
        };

        return bookDetails;
    }

    public static BookItem MapToBookItem(BookData bookData)
    {
        var bookDetails = new BookItem
        {
            Id = bookData.Id,
            Name = bookData.Name,
            Language = bookData.Language.ToString(),
            CoverImage = bookData.CoverImage,
            Price = bookData.Price
        };

        return bookDetails;
    }
}
