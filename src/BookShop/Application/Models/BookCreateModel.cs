using BookShop.Infrastructure.Enums;

namespace BookShop.Application.Models;

public class BookCreateModel
{
    public int CategoryId { get; set; }
    public required string Name { get; set; }

    public required string FileName { get; set; }

    public LanguageType Language { get; set; }

    public required string Description { get; set; }

    public int Price { get; set; }

    public required string Author { get; set; }
    public int Year { get; set; }
    public int Pages { get; set; }

    public byte[]? CoverImage { get; set; }
}
