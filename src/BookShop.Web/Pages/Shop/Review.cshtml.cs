using BookShop.Application;
using BookShop.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookShop.Web.Pages.Shop;

public class ReviewModel : PageModel
{
    private readonly IBookService _bookService;

    public ReviewModel(IBookService bookService)
    {
        _bookService = bookService;
    }

    public BookDetails Output { get; set; }
    public void OnGet(int bookId)
    {
        Output = _bookService.GetDetails(bookId);
    }
}
