using BookShop.Infrastructure.Enums;

namespace BookShop.Application.Models;

public class BookEditModel
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required string FileName { get; set; }

    public int CategoryId { get; set; }
    public LanguageType Language { get; set; }

    public byte[]? CoverImage { get; set; }
}
