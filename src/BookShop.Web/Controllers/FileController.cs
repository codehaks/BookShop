using System.Security.Claims;
using BookShop.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Web.Controllers;

public class FileController : ControllerBase
{
    private readonly IBookService _bookService;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IOrderService _orderService;

    public FileController(IBookService bookService, IWebHostEnvironment webHostEnvironment, IOrderService orderService)
    {
        _bookService = bookService;
        _webHostEnvironment = webHostEnvironment;
        _orderService = orderService;
    }

    [Authorize]
    [Route("api/book/download/{bookid}")]
    public IActionResult Download(int bookId) // REST
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        var currentUserId = userIdClaim.Value;

        var order = _orderService.GetUserBook(currentUserId, bookId);

        if (order is not null)
        {
            var book = _bookService.GetDetails(bookId);
            var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Files", book.FileName);
            var content = System.IO.File.ReadAllBytes(path);

            return File(content, "application/pdf", book.FileName);
        }

        return NotFound();
    }
}
