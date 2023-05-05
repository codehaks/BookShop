using BookShop.Infrastructure.DataModels;

namespace BookShop.Application.Models;

public class BookEditModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string FileName { get; set; }

    public int CategoryId { get; set; }
    public LanguageType Language { get; set; }

    public byte[]? CoverImage { get; set; }
}
