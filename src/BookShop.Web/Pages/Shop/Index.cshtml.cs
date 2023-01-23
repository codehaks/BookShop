using BookShop.Application;
using BookShop.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookShop.Web.Pages.Shop;

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

    [BindProperty]
    public string Term { get; set; }

    public void OnPost()
    {
        BookList = _bookService.GetAll(Term);       
    }
}
