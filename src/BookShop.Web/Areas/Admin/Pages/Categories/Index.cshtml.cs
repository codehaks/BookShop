using BookShop.Infrastructure;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookShop.Web.Areas.Admin.Pages.Categories;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _db;

    public IndexModel(ApplicationDbContext db)
    {
        _db = db;
    }

    [BindProperty]
    public IList<CategoryViewModel> Categories { get; set; }
    public void OnGet()
    {
        Categories = _db.Categories.Select(c => new CategoryViewModel
        {
            Id = c.Id,
            Name = c.Name,
            OriginalName = c.Name
        }).ToList();
    }

    public IActionResult OnPost()
    {
        foreach (var category in Categories)
        {
            if (category.OriginalName != category.Name && category.Id != 0)
            {
                var bookCategory = category.Adapt<BookCategory>();
                _db.Entry(bookCategory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }

            if (category.Id != 0 && category.IsRemoved == true)
            {
                var bookCategory = category.Adapt<BookCategory>();
                _db.Entry(bookCategory).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            }

            if (category.Id == 0 && string.IsNullOrEmpty(category.OriginalName) && string.IsNullOrEmpty(category.Name)==false)
            {
                var bookCategory = category.Adapt<BookCategory>();
                _db.Entry(bookCategory).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            }
        }
        _db.SaveChanges();
        return RedirectToPage("./index");
    }
}

public class CategoryViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }

    public bool IsRemoved { get; set; }
    public string OriginalName { get; set; }
}