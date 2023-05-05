using System.Security.Claims;
using BookShop.Application;
using BookShop.Application.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookShop.Web.Areas.User.Pages.Orders;

public class IndexModel : PageModel
{
    private readonly IOrderService _orderService;

    public IndexModel(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public IList<UserOrderItem> OrderList { get; set; }

    public void OnGet()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        var userId = userIdClaim.Value;

        OrderList = _orderService.GetAllByUser(userId);
    }
}
