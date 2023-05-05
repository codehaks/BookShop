using BookShop.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookShop.Web.Areas.User.Pages.Orders;

public class RateModel : PageModel
{
    private readonly IOrderService _orderService;

    public RateModel(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [BindProperty]
    public int Score { get; set; }

    [BindProperty]
    public int OrderId { get; set; }

    public void OnGet(int orderId)
    {
        orderId = OrderId;
    }

    public IActionResult OnPost()
    {
        _orderService.AddRating(OrderId, Score);
        return RedirectToPage("./index");
    }
}
