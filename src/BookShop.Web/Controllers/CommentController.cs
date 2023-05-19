using System.Security.Claims;
using BookShop.Application;
using BookShop.Application.Interfaces;
using BookShop.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Web.Controllers;

public class CommentController : Controller
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet]
    [Route("api/comment/{bookid}")]
    public IActionResult GetAllByBook(int bookId)
    {
        var comments = _commentService.GetAllByBook(bookId);
        return PartialView("_BookComments", comments);
    }

    [HttpPost]
    [Route("api/comment")]
    public IActionResult PostNewComment(string note, int bookId)
    {
        var userName = User.Identity.Name;

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        var userId = userIdClaim.Value;

        _commentService.Create(userId, userName, bookId, note);
        var output = new CommentOutput
        {
            Note = note,
            UserId = userId,
            UserName = userName,
            BookId = bookId,
            TimeCreated = DateTime.Now
        };
        return PartialView("_LastComment", output);
    }
}
