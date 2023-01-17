using BookShop.Application;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BookShop.Web.Areas.Admin.Pages.Books;

public class EditModel : PageModel
{
    private readonly IBookService _bookService;

    public EditModel(IBookService bookService)
    {
        _bookService = bookService;
    }

    [BindProperty]
    public BookEditInput Input { get; set; }
    public void OnGet(int bookId)
    {
        Input = _bookService.GetDetails(bookId).Adapt<BookEditInput>();
    }
}

public class BookEditInput
{
    public int Id { get; set; }

    [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Must have a name")]
    public string Name { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }
    public int Price { get; set; }

    [MaxLength(250)]
    public string Author { get; set; }
    public int Year { get; set; }

    [Range(1, 5000, ErrorMessage = "Pages must be between {1} and {2} ")]
    public int Pages { get; set; }

    public IFormFile CoverImage { get; set; }
}