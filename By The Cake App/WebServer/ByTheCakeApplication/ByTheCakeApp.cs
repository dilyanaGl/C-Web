using HTTPServer.ByTheCakeApplication.Data;
using HTTPServer.ByTheCakeApplication.ViewModels;
using HTTPServer.ByTheCakeApplication.ViewModels.Account;
using HTTPServer.ByTheCakeApplication.ViewModels.Product;
using HTTPServer.Server.Http.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HTTPServer.ByTheCakeApplication
{
    using Controllers;
    using Server.Contracts;
    using Server.Routing.Contracts;

    public class ByTheCakeApp : IApplication
    {
        public void InitialiseDatabase()
        {
            using (var db = new ByTheCakeDbContext())
            {
              //db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }


        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig
                .Get("/", req => new HomeController().Index());

            appRouteConfig
                .Get("/about", req => new HomeController().About());

            appRouteConfig
                .Get("/add", req => new CakesController().Add());

            appRouteConfig
                .Post(
                    "/add",
                    req => new CakesController().Add(new AddProductViewModel()
                    {
                        Name = req.FormData["name"],
                        Price = decimal.Parse(req.FormData["price"]),
                        ImageUrl = req.FormData["imageUrl"]
                    }));

            appRouteConfig
                .Get(
                    "/search", 
                    req => new CakesController().Search(req));

            appRouteConfig
                .Get(
                    "/register",
                    req => new AccountController().Register());

            appRouteConfig
                .Post(
                    "/register",
                    req => new AccountController().Register(req, new RegisterViewModel()
                    {
                        Username = req.FormData["username"],
                        Password = req.FormData["password"],
                        ConfirmPassword = req.FormData["confirm-password"]
                    }));

            appRouteConfig
                .Get(
                    "/login",
                    req => new AccountController().Login());

            appRouteConfig
                .Post(
                    "/login",
                    req => new AccountController().Login(req, 
                        new LoginViewModel()
                        {
                            Username = req.FormData["username"],
                            Password = req.FormData["password"]
                        }));

            appRouteConfig
                .Post(
                    "/logout",
                    req => new AccountController().Logout(req));

            appRouteConfig.Get(
                "/profile",
                req => new AccountController().Profile(req));

            appRouteConfig
                .Get(
                    "/shopping/add/{(?<id>[0-9]+)}",
                    req => new ShoppingController().AddToCart(req));

            appRouteConfig
                .Get(
                    "/cart",
                    req => new ShoppingController().ShowCart(req));

            appRouteConfig
                .Post(
                    "/shopping/finish-order",
                    req => new ShoppingController().FinishOrder(req));

            appRouteConfig
                .Get("/cakes/{(?<id>[0-9]+)}",
                    req => new CakesController()
                        .Details(int.Parse(req.UrlParameters["id"])));

            appRouteConfig
                .Get("/orders", 
                    req => new ShoppingController().ShowOrders(req));

            appRouteConfig
                .Get("/orderDetails/{(?<id>[0-9]+)}", 
                    req => new ShoppingController().ShowOrderDetails(int.Parse(req.UrlParameters["id"])));
        }
    }
}
