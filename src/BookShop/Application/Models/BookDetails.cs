using BookShop.Infrastructure.DataModels;

namespace BookShop.Application.Models;

public class BookDetails
{
    public int Id { get; set; }

    public ICollection<RatingData> Ratings { get; set; }
    public string Name { get; set; }

    public string FileName { get; set; }

    public string Language { get; set; }
    public byte[]? CoverImage { get; set; }
    public string Description { get; set; }

    public int Price { get; set; }

    public string Author { get; set; }
    public int Year { get; set; }
    public int Pages { get; set; }

 
}
