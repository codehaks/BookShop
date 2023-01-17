using BookShop.Application;
using BookShop.Infrastructure.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace BookShop.Web.Areas.Admin.Pages.Books;

public class CreateModel : PageModel
{
    private readonly IBookService _bookService;

    public CreateModel(IBookService bookService)
    {
        _bookService = bookService;
    }

    [BindProperty]
    public BookInput Input { get; set; }

    public IActionResult OnPost()
    {
        if (Input.Year > DateTime.Now.Year)
        {
            ModelState.AddModelError("Year", "Not valid");
        }
       
        if (ModelState.IsValid==false)
        {
            ModelState.AddModelError(nameof(Pages), "Can not create Book!");
            return Page();
        }

        _bookService.Create(new Application.Models.BookCreateModel
        {
            Author = Input.Author,
            Description = Input.Description,
            Name = Input.Name,
            Pages = Input.Pages,
            Price = Input.Price,
            Year = Input.Year

        });

        return RedirectToPage("./index");
    }
}

public class BookInput
{
    [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Must have a name")]
    public string Name { get; set; }

    public string Description { get; set; }
    public int Price { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }

    [Range(1, 5000, ErrorMessage = "Pages must be between {1} and {2} ")]
    public int Pages { get; set; }
}
