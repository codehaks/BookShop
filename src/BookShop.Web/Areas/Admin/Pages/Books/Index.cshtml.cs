using BookShop.Infrastructure.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookShop.Web.Areas.Admin.Pages.Books;

public class IndexModel : PageModel
{

    public IList<BookData> BookList { get; set; }
    public void OnGet()
    {
        BookList = new List<BookData>();
    }
}
