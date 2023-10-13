using System.ComponentModel.DataAnnotations;
using BookShop.Application.Interfaces;
using BookShop.Application.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CategoryId = int;

namespace BookShop.Web.Areas.Admin.Pages.Books;

public class CreateModel(IBookService bookService) : PageModel
{
    public SelectList CategorySelectList { get; set; }

    [BindProperty]
    public BookInput Input { get; set; }

    public void OnGet()
    {
        var categories = bookService.GetAllCategories();
        CategorySelectList = new SelectList(categories, "Id", "Name");
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(nameof(Pages), "Can not create Book!");
            return Page();
        }

        using var ms = new MemoryStream();
        Input.CoverImageFile.CopyTo(ms);
        ms.Position = 0;

        var model = Input.Adapt<BookCreateModel>();
        model.CoverImage = ms.ToArray();
        bookService.Create(model);

        return RedirectToPage("./index");
    }
}

public class BookInput
{
    public CategoryId CategoryId { get; set; }

    [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Must have a name")]
    public string Name { get; set; }

    public string FileName { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }

    public int Price { get; set; }

    [MaxLength(250)]
    public string Author { get; set; }

    public int Year { get; set; }

    [Range(1, 5000, ErrorMessage = "Pages must be between {1} and {2} ")]
    public int Pages { get; set; }

    public LanguageType Language { get; set; }

    public IFormFile CoverImageFile { get; set; }
}
