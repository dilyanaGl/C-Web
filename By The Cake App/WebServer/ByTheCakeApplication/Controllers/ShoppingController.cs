using System.Globalization;
using HTTPServer.ByTheCakeApplication.Service.Product;
using HTTPServer.ByTheCakeApplication.Service.Shopping;
using HTTPServer.ByTheCakeApplication.Service.User;
using HTTPServer.Server.Http;

namespace HTTPServer.ByTheCakeApplication.Controllers
{
    using Data;
    using Models;
    using Infrastructure;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using System.Linq;
    using System;

    public class ShoppingController : Controller
    {
       
        private readonly IProductService products;
        private readonly IUserService users;
        private readonly IShoppingService shopping;

        public ShoppingController()
        {
            this.products = new ProductService();
            this.users = new UserService();
            this.shopping = new ShoppingService();

        }

        public IHttpResponse AddToCart(IHttpRequest req)
        {
            var id = int.Parse(req.UrlParameters["id"]);

            var doesIdExists = this.products.Exists(id);

            if (!doesIdExists)
            {
                return new NotFoundResponse();
            }

            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);
            shoppingCart.ProductIds.Add(id);

            var redirectUrl = "/search";

            const string searchTermKey = "searchTerm";

            if (req.UrlParameters.ContainsKey(searchTermKey))
            {
                redirectUrl = $"{redirectUrl}?{searchTermKey}={req.UrlParameters[searchTermKey]}";
            }

            return new RedirectResponse(redirectUrl);
        }

        public IHttpResponse ShowCart(IHttpRequest req)
        {
            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            if (!shoppingCart.ProductIds.Any())
            {
                this.ViewData["cartItems"] = "No items in your cart";
                this.ViewData["totalCost"] = "0.00";
            }
            else
            {
                var productsInCart = products.FindProducts(shoppingCart.ProductIds);

                var items = productsInCart
                    .Select(i => $"<div>{i.Name} - ${i.Price:F2}</div><br />");

                var totalPrice = productsInCart
                    .Sum(i => i.Price);
                
                this.ViewData["cartItems"] = string.Join(string.Empty, items);
                this.ViewData["totalCost"] = $"{totalPrice:F2}";
            }

            return this.FileViewResponse(@"shopping\cart");
        }

        public IHttpResponse FinishOrder(IHttpRequest req)
        {
            var username = req.Session.Get<string>(SessionStore.CurrentUserKey);

            var shoppingCart =  req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            int? id = this.users.GetUserId(username);

            if (id == null)
            {
                return new BadRequestResponse();
            }
            shopping.CreateOrder(id.Value, shoppingCart.ProductIds);

           shoppingCart.ProductIds.Clear();

            return this.FileViewResponse(@"shopping\finish-order");
        }

        public IHttpResponse ShowOrders(IHttpRequest req)
        {
            if (!req.Session.Contains(SessionStore.CurrentUserKey))
            {
                throw new InvalidOperationException("There is no logged in user");
            }

            var username = req.Session.Get<string>(SessionStore.CurrentUserKey);

            int? id = this.users.GetUserId(username);

            if (id == null)
            {
                return new BadRequestResponse();
            }

            var orders = shopping.ListOrders(id.Value);

            var itemsToDisplay = orders
                .Select(p =>
                $"<tr>" +
                    $"<td><a href = \"orderDetails/{p.OrderId}\">{p.OrderId}</a></td>" +
                    $"<td>{p.CreatedOn.ToString("dd-MM-yyyy")}</td>" +
                    $"<td>${p.Sum:f2}</td>" +
                $"</tr>"
            );

            this.ViewData["orders"] = String.Join(String.Empty, itemsToDisplay);

            return this.FileViewResponse(@"shopping\orders");
        }

        public IHttpResponse ShowOrderDetails(int id)
        {

            var products = shopping.ListProducts(id);

            var itemsToDisplay = products
                .Select(p =>
                    $"<tr>" +
                       $"<td><a href = \"/cakes/{p.Id}\">{p.ProductName}</a></td>" +
                       $"<td>${p.Price:f2}</td>" +
                    $"</tr>"
                    );

            var creationDate = shopping.GetDate(id);

            this.ViewData["orderId"] = id.ToString();
            this.ViewData["orderDetails"] = String.Join(String.Empty, itemsToDisplay);
            this.ViewData["creationDate"] = creationDate.Value.ToString("dd-MM-yyyy");


            return this.FileViewResponse(@"shopping\orderDetails");
        }
    }
}

