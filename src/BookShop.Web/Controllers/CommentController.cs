using BookShop.Application;
using BookShop.Web.Common;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        var userName = User.GetUserName();

        _commentService.Create(User.GetUserId(), userName, bookId, note);
        var output = new CommentOutput
        {
            Note = note,
            UserId= User.GetUserId(),
            UserName=userName,
            BookId=bookId,
            TimeCreated=DateTime.Now
        };
        return PartialView("_LastComment", output);
    }
}
