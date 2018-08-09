using System;
using System.Linq;
using System.Text;
using HTTPServer.GameWorkshop.Infrastructure;
using HTTPServer.GameWorkshop.Services;
using HTTPServer.GameWorkshop.Services.Contracts;
using HTTPServer.Server.Http;
using HTTPServer.Server.Http.Contracts;
using HTTPServer.Server.Http.Response;

namespace HTTPServer.GameWorkshop.Controllers
{
    public class ShoppingController : Controller
    {
        private const string CartPath = @"Shopping\cart";
        private readonly IShoppingService shoppingService;


        public ShoppingController(IHttpRequest req) : base(req)
        {
            this.shoppingService = new ShoppingService();
        }

        public IHttpResponse AddToCart()
        {
            int id = int.Parse(this.Request.UrlParameters["id"]);

            var email = Request.Session.Get<string>(SessionStore.CurrentUserKey);

            if (email == null)
            {
                return new RedirectResponse("/login");
            }

            var success = this.shoppingService.AddToCart(email, id);

            if (!success)
            {
                this.AddError("You cannot purchase the same game twice!");
                return new RedirectResponse($"/game/{id}");
            }

            return new RedirectResponse("/order/cart");
        }

        public IHttpResponse ShowCart()
        {
            var orders = this.shoppingService.ListItems(Request.Session.Get<string>(SessionStore.CurrentUserKey));

            var sb = new StringBuilder();
            
            foreach (var order in orders)
            {
               

                sb.AppendLine($"<div class=\"list-group\">" +
                              $"<div class=\"list-group-item\"> " +
                              $"<div class=\"media\">" +
                              $"<a class=\"btn btn - outline - danger btn - lg align - self - center mr - 3\"" +
                              $"href=\"/order/remove/{order.Id}\">" +
                              $"X" +
                              $"</a> " +
                              $"<img class=\"d-flex mr-4 align-self-center img-thumbnail\"" +
                              $"height=\"127\" src=\"{order.ImageThumbnail}\"width=\"227\"" +
                              $"alt=\"Generic placeholder image\">" +
                              $"<div class=\"media-body align-self-center\">" +
                              $"<a href = \"#\" >" +
                              $"<h4 class=\"mb-1 list-group-item-heading\"> " +
                              $"{order.Title}" +
                              $"</h4> " +
                              $"</a>" +
                              $"<p>" +
                              $"{order.Description}" +
                              $"</p>" +
                              $"</div>" +
                              $"<div class=\"col-md-2 text-center align-self-center mr-auto\">" +
                              $"<h2> {order.Price}&euro; " +
                              $"</h2>" +
                              $"</div> " +
                              $"</div> " +
                              $"</div> " +
                              $"</div>");
            }

            this.ViewData["content"] = sb.ToString();
            this.ViewData["total"] = orders.Sum(p => p.Price).ToString("f2");

            return this.FileViewResponse(CartPath);
        }

        public IHttpResponse Remove()
        {
            int id = Int32.Parse(this.Request.UrlParameters["id"]);

            this.shoppingService.Remove(Request.Session.Get<string>(SessionStore.CurrentUserKey),id);

            return new RedirectResponse("/order/cart");
        }

        public IHttpResponse FinishOrder()
        {
            shoppingService.Clear(Request.Session.Get<string>(SessionStore.CurrentUserKey));

            return this.FileViewResponse(@"Shopping\finish-order");
        }
    }
}
