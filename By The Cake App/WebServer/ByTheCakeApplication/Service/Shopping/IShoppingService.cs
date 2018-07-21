using System;
using System.Collections.Generic;
using HTTPServer.ByTheCakeApplication.ViewModels.Orders;

namespace HTTPServer.ByTheCakeApplication.Service.Shopping
{
    public interface IShoppingService
    {
        void CreateOrder(int userId, IEnumerable<int> ids);

       IEnumerable<OrderListViewModel> ListOrders(int userId);

        IEnumerable<OrderDetailsViewModel> ListProducts(int orderId);

        DateTime? GetDate(int orderId);

    }
}
