using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTTPServer.ByTheCakeApplication.Data;
using HTTPServer.ByTheCakeApplication.Data.Models;
using HTTPServer.ByTheCakeApplication.ViewModels.Orders;

namespace HTTPServer.ByTheCakeApplication.Service.Shopping
{
    public class ShoppingService : IShoppingService
    {
        public void CreateOrder(int userId, IEnumerable<int>ids)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var order = new Order
                {
                    UserId = userId,
                    CreationDate = DateTime.Now,
                    OrderProducts = ids.Select(p => new OrderProduct()
                    {
                        ProductId = p

                    }).ToList(),
                    
                };

                db.Add(order);
                db.SaveChanges();
            }
        }

        public IEnumerable<OrderListViewModel> ListOrders(int userId)
        {
            using (var db = new ByTheCakeDbContext())
            {
               return db.Orders.Where(p => p.UserId == userId)
                    .Select(p => new OrderListViewModel()
                    {
                        OrderId = p.Id,
                        CreatedOn = p.CreationDate,
                        Sum = p.OrderProducts.Sum(k => k.Product.Price)
                    })
                    .ToArray();
            }
        }

        public IEnumerable<OrderDetailsViewModel> ListProducts(int orderId)
        {
            using (var db = new ByTheCakeDbContext())
            {
                return db
                    .OrderProducts
                    .Where(p => p.OrderId == orderId)
                    .Select(p => new OrderDetailsViewModel()
                    {
                        Id = p.ProductId,
                        ProductName = p.Product.Name,
                        Price = p.Product.Price
                    })
                    .ToArray();

            }
        }

        public DateTime? GetDate(int orderId)
        {
            using (var db = new ByTheCakeDbContext())
            {
                return db.Orders.FirstOrDefault(p => p.Id == orderId)?.CreationDate;
            }
        }
    }
}

