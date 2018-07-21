using HTTPServer.ByTheCakeApplication.Service.Product;
using HTTPServer.ByTheCakeApplication.ViewModels.Product;
using HTTPServer.Server.Http.Response;

namespace HTTPServer.ByTheCakeApplication.Controllers
{
    using Data;
    using Infrastructure;
    using Models;
    using Server.Http.Contracts;
    using System;
    using System.Linq;

    public class CakesController : Controller
    {
        private readonly CakesData cakesData;
        private readonly IProductService products;

        public CakesController()
        {
            this.cakesData = new CakesData();
            this.products = new ProductService();

        }

        public IHttpResponse Add()
        {
            this.ViewData["showResult"] = "none";

            return this.FileViewResponse(@"cakes\add");
        }

        public IHttpResponse Add(AddProductViewModel model)
        {

            if (model.Name.Length < 3 || model.Name.Length > 30
                                      || model.ImageUrl.Length < 3 || model.ImageUrl.Length > 2000)
            {
                ApplyError("Invalid product description");
                return this.FileViewResponse(@"cakes\add");
            }

            this.products.Create(model.Name, model.Price, model.ImageUrl);

            this.ViewData["name"] = model.Name;
            this.ViewData["price"] = Convert.ToString(model.Price);

            this.ViewData["imageUrl"] = model.ImageUrl;
            this.ViewData["showResult"] = "block";

            return this.FileViewResponse(@"cakes\add");
        }

        public IHttpResponse Search(IHttpRequest req)
        {
            const string searchTermKey = "searchTerm";

            var urlParameters = req.UrlParameters;

            this.ViewData["results"] = string.Empty;
            this.ViewData["searchTerm"] = string.Empty;

            var searchTerm = urlParameters.ContainsKey(searchTermKey)
                ? urlParameters[searchTermKey]
                : null;

            var result = this.products.List(searchTerm);

            if (result == null)
            {
                this.ViewData["results"] = "No cakes found";
            }
            else
            {
                var allProducts = result.Select(c =>
                    $@"<div><a href = ""/cakes/{c.Id}"">{c.Name}</a> - ${c.Price:F2} <a href=""/shopping/add/{c.Id}?searchTerm={
                            searchTerm
                        }"">Order</a></div>");

                this.ViewData["results"] = String.Join(Environment.NewLine, allProducts);
            }
            
            this.ViewData["showCart"] = "none";

            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            if (shoppingCart.ProductIds.Any())
            {
                var totalProducts = shoppingCart.ProductIds.Count;
                var totalProductsText = totalProducts != 1 ? "products" : "product";

                this.ViewData["showCart"] = "block";
                this.ViewData["products"] = $"{totalProducts} {totalProductsText}";
            }

            return this.FileViewResponse(@"cakes\search");
        }

        public IHttpResponse Details(int id)
        {
            var product = products.Find(id);

            if (product == null)
            {
                return new NotFoundResponse(); 
            }

            this.ViewData["name"] = product.Name;
            this.ViewData["price"] =product.Price.ToString("F2");
            this.ViewData["imageUrl"] = product.ImageUrl;

            return FileViewResponse(@"cakes\details");
        }
    }
}
