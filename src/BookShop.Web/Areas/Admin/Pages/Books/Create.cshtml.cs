using BookShop.Infrastructure.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookShop.Web.Areas.Admin.Pages.Books;

public class CreateModel : PageModel
{

    public string Name { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
    public int Pages { get; set; }
    public void OnGet()
    {
      
    }
}
