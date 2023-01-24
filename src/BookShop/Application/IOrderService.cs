using BookShop.Application.Models;

namespace BookShop.Application
{
    public interface IOrderService
    {
        void Confirm(int orderId);
        int Create(OrderCreateModel model);
        OrderDetails Get(int orderId);
    }
}