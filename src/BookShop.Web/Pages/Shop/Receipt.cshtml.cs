using BookShop.Application;
using BookShop.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookShop.Web.Pages.Shop
{
    public class ReceiptModel : PageModel
    {
        private readonly IOrderService _orderService;

        public ReceiptModel(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public OrderDetails Output { get; set; }
        public void OnGet()
        {
            var tempOrderId =TempData[Values.OrderId];

            var orderId = int.Parse(tempOrderId.ToString());

            Output = _orderService.Get(orderId);

        }
    }
}
