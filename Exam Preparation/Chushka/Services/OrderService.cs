using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Chushka.Services
{
    using Data;
    using Models;
    using Data.Models;

    public class OrderService : IOrderService
    {
        private readonly ChushkaDbContext context;

        public OrderService()
        {
            this.context = new ChushkaDbContext();

        }

        public AllOrdersViewModel All()
        {

            var orders = this.context.Orders.Select(
            p => new OrderListModel
            {
                Id = p.Id,
                Customer = p.Client.Username,
                Product = p.Product.Name,
                OrderedOn = p.OrderedOn.ToString("hh:mm dd/MM/yyyy")
            })
            .ToArray();

            var model = new AllOrdersViewModel
            {
                Orders = orders
            };

            return model;
        }

        public bool Order(int productId, string username)
        {

            var user = this.context.Users.SingleOrDefault(p => p.Username == username);
            var product = this.context.Products.SingleOrDefault(p => p.Id == productId);

            if (user == null || product == null)
            {
                return false;

            }

            var order = new Order
            {

                Product = product,
                Client = user,
                OrderedOn = DateTime.UtcNow

            };

            this.context.Orders.Add(order);

            this.context.SaveChanges();

            return true;
        }
    }
}
