using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace BookShop.Web.Areas.User.Pages;

public class IndexModel : PageModel
{
    public string? UserName { get; set; }
    public string UserId { get; set; }
    public void OnGet()
    {
       UserName = User.Identity!.Name;
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        UserId = userIdClaim.Value;

        
     
    }


}
