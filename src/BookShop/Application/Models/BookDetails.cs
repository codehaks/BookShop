using BookShop.Infrastructure.DataModels;

namespace BookShop.Application.Models;

public record BookDetails
{
    public int Id { get; set; }

    public ICollection<RatingData>? Ratings { get; set; }
    public required string Name { get; set; }

    public required string FileName { get; set; }

    public required string Language { get; set; }
    public byte[]? CoverImage { get; set; }
    public required string Description { get; set; }

    public int Price { get; set; }

    public required string Author { get; set; }
    public int Year { get; set; }
    public int Pages { get; set; }

    //public void Deconstruct(out string name,out int year)
    //{
    //    name = Name;
    //    year = Year;
    //}

    //public void Deconstruct(out string name, out int year,out string language)
    //{
    //    name = Name;
    //    year = Year;
    //    language = Language;
    //}
    
}
