using BookShop.Application.Interfaces;
using BookShop.Application.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookShop.Web.Areas.Admin.Pages.Books;

public class DetailsModel : PageModel
{
    private readonly IBookService _bookService;
    private readonly ILogger<DetailsModel> _logger;

    public DetailsModel(IBookService bookService, ILogger<DetailsModel> logger)
    {
        _bookService = bookService;
        _logger = logger;
    }

    public BookDetails Output { get; set; }

    public void OnGet(int bookId)
    {

        if (bookId <= 0)
        {
            _logger.LogError("Invalid book ID: {BookId}", bookId);
            return;
        }

        _logger.LogInformation("Fetching details for book with ID: {BookId}", bookId);

        Output = _bookService.GetDetails(bookId);
        if (Output == null)
        {
            _logger.LogInformation("No details found for book with ID: {BookId}", bookId);
            return;
        }
    }
}
