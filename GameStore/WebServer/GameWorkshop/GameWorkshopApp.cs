using System;
using HTTPServer.GameWorkshop.Controllers;
using HTTPServer.GameWorkshop.Data;
using HTTPServer.GameWorkshop.ViewModels.Account;
using HTTPServer.GameWorkshop.ViewModels.Admin;
using HTTPServer.Server.Contracts;
using HTTPServer.Server.Http.Contracts;
using HTTPServer.Server.Routing.Contracts;

namespace HTTPServer.GameWorkshop
{
    public class ByTheCakeApp : IApplication
    {
        public ByTheCakeApp ()
        {
            InitialiseDatabase();
        }

        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig.AnonymousPath.Add("/account/login");
            appRouteConfig.AnonymousPath.Add("/account/register");

            appRouteConfig
                .Get("/", req => new HomeController(req).Home());
            
            appRouteConfig
                .Get(
                    "/account/login",
                    req => new AccountController(req).Login());

            appRouteConfig
                .Post(
                    "/account/login",
                    req => new AccountController(req).Login(new LoginViewModel
                    {
                        Email = req.FormData["email"], 
                        Password = req.FormData["password"]
                    }));

            appRouteConfig
                .Get(
                    "/account/logout",
                    req => new AccountController(req).Logout());

          appRouteConfig
                .Get(
                    "/account/register",
                    req => new AccountController(req).Register());

            appRouteConfig
                .Post(
                    "/account/register",
                    req=> new AccountController(req).Register(new RegisterViewModel()
                    {
                        Email = req.FormData["email"],
                        Password = req.FormData["password"],
                        ConfirmPassword = req.FormData["confirmPassword"],
                        FullName = req.FormData["fullName"]
                    }));

            appRouteConfig
                .Get(
                    "/admin/addGame",
                req => new AdminController(req).Add());

            appRouteConfig
                .Post(
                    "/admin/addGame",
                req => new AdminController(req).Add(
                    new AddGameViewModel()
                    {
                        Title = req.FormData["title"],
                        Description = req.FormData["description"],
                        ImageThumbnail = req.FormData["image"],
                        Price = decimal.Parse(req.FormData["price"]),
                        Size = double.Parse(req.FormData["size"]),
                        YouTubeVideoId = req.FormData["video"],
                        ReleaseDate = DateTime.Parse(req.FormData["date"])

                    }));

            appRouteConfig
                .Get(
                    "/admin/list",
                    req => new AdminController(req).List());

            appRouteConfig
                .Get(
                    "/admin/edit/{(?<id>[0-9]+)}",
                    req=> new AdminController(req).Edit());

            appRouteConfig
                .Post(
                    "/admin/edit/{(?<id>[0-9]+)}",
                    req => new AdminController(req).Edit(new EditGamesViewModel()
                    {
                        Id = int.Parse(req.UrlParameters["id"]),
                        Title = req.FormData["name"],
                        ImageThumbnail = req.FormData["image"],
                        Price = decimal.Parse(req.FormData["price"]),
                        Size = double.Parse(req.FormData["size"]),
                        Description = req.FormData["description"],
                        ReleaseDate = DateTime.Parse(req.FormData["date"]),
                        YouTubeVideoId = req.FormData["video"]

                    }));

            appRouteConfig
                .Get(
                    "/admin/delete/{(?<id>[0-9]+)}",
                    req => new AdminController(req).Delete());

            appRouteConfig
                .Post(
                "/admin/delete/{(?<id>[0-9]+)}",
                req => new AdminController(req).Delete(int.Parse(req.UrlParameters["id"])));

            appRouteConfig
                .Get(
                    "/game/{(?<id>[0-9]+)}",
                    req => new GameController(req).Details());

            appRouteConfig
                .Get(
                    "/order/{(?<id>[0-9]+)}",
                    req => new ShoppingController(req).AddToCart());

            appRouteConfig
                .Get(
                    "/order/cart",
                    req => new ShoppingController(req).ShowCart());

            appRouteConfig
                .Get(
                    "/order/remove/{(?<id>[0-9]+)}",
                    req=> new ShoppingController(req).Remove());

            appRouteConfig
                .Post("/order/FinishOrder",
                    req => new ShoppingController(req).FinishOrder());
        }

        private void InitialiseDatabase()
        {
            using (var db = new GameDbContext())
            {
               // db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }
    }
}
