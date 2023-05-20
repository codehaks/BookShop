namespace BookShop.Application.Models;

public class BookItem
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public int Price { get; init; }
    public string? CategoryName { get; set; }

    public required string Language { get; set; }
    public byte[]? CoverImage { get; set; }
}
