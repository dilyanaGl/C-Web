using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Chushka.Services
{
    using Data;
    using Models;
    using Data.Models;

    public class ProductService : IProductService
    {
        private readonly ChushkaDbContext context;

        public ProductService()
        {
            this.context = new ChushkaDbContext();

        }

        public IndexViewModel Index()
        {

            var products = this.context.Products.Select(p => new ProductListModel
            {
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Id = p.Id

            })
            .ToArray();

            var model = new IndexViewModel
            {
                Products = products

            };

            return model;
        }

        public ProductDetailsModel Details(int id)
        {

            return this.context.Products.Where(p => p.Id == id)
            .Select(p => new ProductDetailsModel
            {
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Type = p.Type.ToString(), 
                Id = id
            })
            .SingleOrDefault();

        }

        public void CreateProduct(CreateProductViewModel model)
        {

            var type = Enum.Parse<ProductType>(model.ProductType, true);

            var product = new Product
            {
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                Type = type
            };


            this.context.Products.Add(product);

            this.context.SaveChanges();

        }

        public EditProductViewModel DisplayProduct(int id)
        {

            return this.context.Products.Where(p => p.Id == id)
            .Select(p => new EditProductViewModel
            {
                Name = p.Name,
                Price = p.Price.ToString("f2"),
                Description = p.Description,
                ProductType = p.Type.ToString(), 
                Id = id

            })
            .SingleOrDefault();

        }

        public void Edit(EditProductViewModel model)
        {

            var product = this.context.Products.SingleOrDefault(p => p.Id == model.Id);

            product.Name = model.Name;
            product.Price = decimal.Parse(model.Price);
            product.Description = model.Description;
            product.Type = Enum.Parse<ProductType>(model.ProductType, true);

            context.SaveChanges();

        }

        public void DeleteProduct(int id)
        {

            var product = this.context.Products.SingleOrDefault(p => p.Id == id);

            this.context.Products.Remove(product);
            this.context.SaveChanges();

        }

        public void Order(int id, string username)
        {
            var user = this.context.Users.SingleOrDefault(p => p.Username == username);
            var prodict = this.context.Products.SingleOrDefault(p => p.Id == id);
            if(user == null || prodict == null)
            {
                return;
            }
            var order = new Order()
            {
                Client = user,
                OrderedOn = DateTime.UtcNow,
                Product = prodict
            };

            this.context.Orders.Add(order);
            this.context.SaveChanges();


        }
    }
}
