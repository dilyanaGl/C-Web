using System;
using System.Collections.Generic;
using System.Text;
using HTTPServer.GameWorkshop.Infrastructure;
using HTTPServer.GameWorkshop.Services;
using HTTPServer.GameWorkshop.Services.Contracts;
using HTTPServer.Server.Http.Contracts;

namespace HTTPServer.GameWorkshop.Controllers
{
    public class GameController : Controller
    {
        private const string DetailsPath = @"Game\GameDetails";

        private readonly IGameService gameService;


        public GameController(IHttpRequest request) : base(request)
        {
            this.gameService = new GameService();
        }

        public IHttpResponse Details()
        {
            int id = int.Parse(this.Request.UrlParameters["id"]);
            var game = gameService.GetDetails(id);
            string buttonDisplay = "none";
            var isAdmin = this.Authentication.IsAdmin;

            if (isAdmin)
            {
                buttonDisplay = "initial";
            }

            this.ViewData["name"] = game.Title;
            this.ViewData["description"] = game.Description;
            this.ViewData["price"] = game.Price.ToString();
            this.ViewData["size"] = game.Size.ToString();
            this.ViewData["date"] = game.ReleaseDate.ToString();
            this.ViewData["url"] = game.YouTubeViewId;
            this.ViewData["isAdmin"] = buttonDisplay;
            this.ViewData["id"] = id.ToString();
            
            return this.FileViewResponse(DetailsPath);
        }
    }
}
