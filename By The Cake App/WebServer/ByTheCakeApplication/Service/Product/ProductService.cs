using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTTPServer.ByTheCakeApplication.Data;
using HTTPServer.ByTheCakeApplication.Data.Models;
using HTTPServer.ByTheCakeApplication.ViewModels.Product;


namespace HTTPServer.ByTheCakeApplication.Service.Product
{
    public class ProductService : IProductService
    {
        public void Create(string name, decimal price, string imageUrl)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var product = new Data.Models.Product()
                {
                    Name = name,
                    Price = price,
                    ImageUrl = imageUrl
                };

                db.Add(product);
                db.SaveChanges();
            }
        }

        public IEnumerable<ProductListingViewModel> List(string searchTerm = null)
        {

            using (var db = new ByTheCakeDbContext())
            {
                var results = db.Products.ToArray();

                if (!String.IsNullOrEmpty(searchTerm))
                {
                    results = results
                        .Where(p => p.Name.ToLower().Contains(searchTerm.ToLower()))
                        .ToArray();
                }

                return results.Select(p => new ProductListingViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                })
                    .ToList();

            }




        }

        public ProductDetailsViewModel Find(int id)
        {
            using (var db = new ByTheCakeDbContext())
            {
                return db.Products
                    .Where(p => p.Id == id)
                    .Select(p => new ProductDetailsViewModel()
                    {
                        Name = p.Name,
                        Price = p.Price,
                        ImageUrl = p.ImageUrl

                    })
                    .FirstOrDefault();
            }
        }

        public bool Exists(int id)
        {
            using (var db = new ByTheCakeDbContext())
            {
                return db.Products.Any(p => p.Id == id);
            }
        }

        public IEnumerable<ProductCartViewModel> FindProducts(IEnumerable<int> ids)
        {
            using (var db = new ByTheCakeDbContext())
            {
                return db.Products
                        .Where(p => ids.Contains(p.Id))
                        .Select(p => new ProductCartViewModel
                        {
                            Name = p.Name,
                            Price = p.Price
                        })
                    .ToList();
            }
        }
    }
}
