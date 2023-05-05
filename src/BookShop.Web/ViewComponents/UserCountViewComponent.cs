using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Web.ViewComponents;

public class UserCountViewComponent : ViewComponent
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserCountViewComponent(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var count = await _userManager.Users.CountAsync();
        return View("Default", count);
    }
}
