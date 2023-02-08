using BookShop.Application;
using BookShop.Application.Models;
using BookShop.Web.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

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
        OrderList = _orderService.GetAllByUser(User.GetUserId());
    }
}
