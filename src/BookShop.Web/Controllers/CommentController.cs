﻿using BookShop.Application;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Web.Controllers;

public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [Route("api/comment/{bookid}")]
    public IActionResult GetAllByBook(int bookId)
    {
        var comments=_commentService.GetAllByBook(bookId);
        return Ok(comments);
    }
}