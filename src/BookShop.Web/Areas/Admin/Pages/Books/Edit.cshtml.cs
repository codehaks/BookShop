using BookShop.Application;
using BookShop.Application.Models;
using BookShop.Infrastructure.DataModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;
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

    public byte[]? CoverImageContent { get; set; }

    public SelectList CategorySelectList { get; set; }
    public void OnGet(int bookId)
    {
        var categories=_bookService.GetAllCategories();
        CategorySelectList = new SelectList(categories, "Id", "Name");
        var model = _bookService.GetEdit(bookId);
        Input =model.Adapt<BookEditInput>();
        CoverImageContent = model.CoverImage;
    }

    public IActionResult OnPost()
    {
        var creatModel = Input.Adapt<BookEditModel>();

        if (Input.CoverImageFile is not null)
        {
            using var ms = new MemoryStream();
            Input.CoverImageFile.CopyTo(ms);
            ms.Position = 0;
            creatModel.CoverImage = ms.ToArray();
        }
        _bookService.Update(creatModel.Adapt<BookEditModel>());
        return RedirectToPage("./index");
    }

}

public class BookEditInput
{
    public int Id { get; set; }

    [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Must have a name")]
    public string Name { get; set; }
    public string FileName { get; set; }
    public LanguageType Language { get; set; }

    public int CategoryId { get; set; }

    public string? AuthorName { get; set; }
    public string? AuthorEmail { get; set; }

    public IFormFile? CoverImageFile { get; set; }
}