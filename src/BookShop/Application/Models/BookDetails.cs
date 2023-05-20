using BookShop.Infrastructure.DataModels;

namespace BookShop.Application.Models;

public class BookDetails
{
    public int Id { get; set; }

    public ICollection<RatingData>? Ratings { get; set; }
    public required string Name { get; set; }

    public required string FileName { get; set; }

    public required string Language { get; set; }
    public byte[]? CoverImage { get; set; }
    public required string Description { get; set; }

    public  int Price { get; set; }

    public required string Author { get; set; }
    public int Year { get; set; }
    public int Pages { get; set; }
}
