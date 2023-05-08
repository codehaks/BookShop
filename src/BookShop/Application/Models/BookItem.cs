namespace BookShop.Application.Models;

public class BookItem
{
    public int Id { get; init; }
    public string Name { get; init; }
    public int Price { get; init; }
    public string CategoryName { get; set; }

    public string Language { get; set; }
    public byte[]? CoverImage { get; set; }
}
