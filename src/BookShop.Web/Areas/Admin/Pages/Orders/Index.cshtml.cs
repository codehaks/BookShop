using BookShop.Application;
using BookShop.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;

namespace BookShop.Web.Areas.Admin.Pages.Orders;

public class IndexModel : PageModel
{
    private readonly IOrderService _orderService;

    public IndexModel(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public IList<OrderItem> OrderList { get; set; }
    public void OnGet()
    {
        OrderList = _orderService.GetAll();
    }
}
