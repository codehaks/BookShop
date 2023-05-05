using System.Security.Claims;
using Ardalis.GuardClauses;
using BookShop.Application;
using BookShop.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookShop.Web.Pages.Shop;

[Authorize]
public class ReviewModel : PageModel
{
    private readonly IBookService _bookService;
    private readonly IOrderService _orderService;

    public ReviewModel(IBookService bookService, IOrderService orderService)
    {
        _bookService = bookService;
        _orderService = orderService;
    }

    public BookDetails Output { get; set; }

    [BindProperty]
    public int BookId { get; set; }

    public void OnGet(int bookId)
    {
        Output = _bookService.GetDetails(bookId);
        BookId = bookId;
    }

    public IActionResult OnPost()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        Guard.Against.Null(userIdClaim);
        var userId = userIdClaim.Value;

        var book = _bookService.GetDetails(BookId);

        // GUID

        var orderId = _orderService.Create(new OrderCreateModel
        {
            BookId = BookId,
            Amount = book.Price,
            UserId = userId
        });

        // Send to Bank/Payment system!

        _orderService.Confirm(orderId);

        TempData[Values.OrderId] = orderId;

        return RedirectToPage("./receipt");
    }
}
