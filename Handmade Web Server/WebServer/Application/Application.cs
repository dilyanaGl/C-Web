using HTTPServer.Application.Contracts;
using HTTPServer.Application.Controllers;
using HTTPServer.Server.Enums;
using HTTPServer.Server.Handlers;
using HTTPServer.Server.Routing.Contracts;

namespace HTTPServer.Application
{
    public class Application : IApplication
    {
        public void Start(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig.AddRoute("/", HttpRequestMethod.Get, new RequestHandler(httpContext => new HomeController().Index()));

            appRouteConfig.AddRoute("/add", HttpRequestMethod.Get, new RequestHandler(httpContext => new CakeController().Add()));

            appRouteConfig.AddRoute("/search", HttpRequestMethod.Get, new RequestHandler(httpContext => new CakeController().Search(httpContext.UrlParameters)));

            appRouteConfig.AddRoute("/addToCart", HttpRequestMethod.Post, new RequestHandler(httpContext => new CakeController().AddToCart(httpContext.FormData["Cake"])));

            //appRouteConfig.AddRoute("/search", HttpRequestMethod.Post,
            //    new RequestHandler(httpContext => new CakeController().Search(httpContext.UrlParameters)));

            appRouteConfig.AddRoute("/about", HttpRequestMethod.Get, new RequestHandler(httpContext => new HomeController().AboutUs()));

            appRouteConfig.AddRoute("/add", HttpRequestMethod.Post, new RequestHandler(httpContext => new CakeController().Add(httpContext.FormData["Name"], httpContext.FormData["Price"])));

           appRouteConfig.AddRoute("/", HttpRequestMethod.Post, new RequestHandler(httpContext => new HomeController().Home(httpContext.FormData["Username"], httpContext.FormData["Password"])));

            appRouteConfig.AddRoute("/shoppingcart", HttpRequestMethod.Get, new RequestHandler(httpContext => new CakeController().GetCart()));

            appRouteConfig.AddRoute("/finishOrder", HttpRequestMethod.Get, new RequestHandler(httpContext => new CakeController().FinishOrder()));




        }
    }
}



