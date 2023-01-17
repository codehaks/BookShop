using BookShop.Infrastructure.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace BookShop.Web.Areas.Admin.Pages.Books;

[BindProperties]
public class CreateModel : PageModel
{
    [StringLength(maximumLength:50, MinimumLength= 2,ErrorMessage ="Must have a name")]
    public string Name { get; set; }

    public string Description { get; set; }
    public int Price { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }

    [Range(1, 5000, ErrorMessage = "Pages must be between {1} and {2} ")]
    public int Pages { get; set; }
    public IActionResult OnPost()
    {
        if (Year>DateTime.Now.Year)
        {
            ModelState.AddModelError("Year", "Not valid");
        }
       
        if (ModelState.IsValid==false)
        {
            ModelState.AddModelError(nameof(Pages), "Can not create Book!");
            return Page();
        }
        // Save to db

        return RedirectToPage("./index");
    }
}
