using System.ComponentModel.DataAnnotations;

namespace BookShop.Infrastructure.DataModels;

public class BookCategory
{
    public int Id { get; set; }
    public required string Name { get; set; }
}

public enum LanguageType
{
    None = 0,
    English = 1,
    Farsi = 2,
    Russian = 3,
    Greek = 4
}

public class BookData
{
    public int Id { get; set; }

    public ICollection<RatingData>? Ratings { get; set; }

    [MaxLength(50)]
    public required string FileName { get; set; }

    public int CategoryId { get; set; }
    public BookCategory? Category { get; set; }

    [MaxLength(50)]
    public required string Name { get; set; }

    public LanguageType Language { get; set; }

    [MaxLength(500)]
    public required string Description { get; set; }

    public int Price { get; set; }

    [MaxLength(250)]
    public required string Author { get; set; }

    public int Year { get; set; }
    public int Pages { get; set; }

    [MaxLength(1_000_000)]
    public byte[]? CoverImage { get; set; }
}
