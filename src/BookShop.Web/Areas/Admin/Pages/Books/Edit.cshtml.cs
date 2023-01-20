using BookShop.Application;
using BookShop.Application.Models;
using BookShop.Infrastructure.DataModels;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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

    public SelectList CategorySelectList { get; set; }
    public void OnGet(int bookId)
    {
        var categories=_bookService.GetAllCategories();
        CategorySelectList = new SelectList(categories, "Id", "Name");
        Input = _bookService.GetEdit(bookId).Adapt<BookEditInput>();
    }

    public IActionResult OnPost()
    {
     _bookService.Update(Input.Adapt<BookEditModel>());
        return RedirectToPage("./index");
    }

}

public class BookEditInput
{
    public int Id { get; set; }

    [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Must have a name")]
    public string Name { get; set; }

    public LanguageType Language { get; set; }

    public int CategoryId { get; set; }
}