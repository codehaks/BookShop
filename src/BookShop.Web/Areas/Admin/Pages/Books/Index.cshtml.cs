using BookShop.Application;
using BookShop.Application.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookShop.Web.Areas.Admin.Pages.Books;

public class IndexModel : PageModel
{
    private readonly IBookService _bookService;

    public IndexModel(IBookService bookService)
    {
        _bookService = bookService;
    }

    public IList<BookItem> BookList { get; set; }

    public void OnGet()
    {
        BookList = _bookService.GetAll();
    }
}
