using BookShop.Application;
using BookShop.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookShop.Web.Areas.Admin.Pages.Books;

public class DetailsModel : PageModel
{
    private readonly IBookService _bookService;

    public DetailsModel(IBookService bookService)
    {
        _bookService = bookService;
    }

    public BookDetails Output { get; set; }
    public void OnGet(int bookId)
    {
       Output= _bookService.GetDetails(bookId);
    }
}
