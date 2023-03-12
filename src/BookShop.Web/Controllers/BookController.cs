using BookShop.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Web.Controllers;
public class BookController : Controller {

    private readonly ApplicationDbContext _db;

    public BookController(ApplicationDbContext db) {
        _db = db;
    }

    [Route("api/book/details/{id}")]
    [HttpGet]
    public IActionResult Details(int id) {
        var book=_db.Books.Find(id);

        return PartialView("_BookDetails",book);
    }
}

