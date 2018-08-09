using System.Text;
using HTTPServer.GameWorkshop.Data.Models;
using HTTPServer.GameWorkshop.Infrastructure;
using HTTPServer.GameWorkshop.Services;
using HTTPServer.GameWorkshop.Services.Contracts;
using HTTPServer.Server.Http;
using HTTPServer.Server.Http.Contracts;

namespace HTTPServer.GameWorkshop.Controllers
{

    public class HomeController : Controller
    {
     private const string HomePath = @"Home\Home";
        private readonly IGameService gameService;

        public HomeController(IHttpRequest request) : base(request)
        {
            this.gameService = new GameService();
        }
            public IHttpResponse Home()
            {
                var sb = new StringBuilder();
                var games = gameService.DisplayHome();
                string isAdmin = "none";
                if (this.Authentication.IsAdmin)
                {
                    isAdmin = "initial";
                }

                foreach (var game in games)
                {
                    sb.AppendLine(
                        $"<div class=\"card col-4 thumbnail\">" +
                        $"<img class=\"card-image-top img-fluid img-thumbnail\"" +
                        $"onerror=\"this.src='https://i.ytimg.com/vi/BqJyluskTfM/maxresdefault.jpg';" +
                        $"\"src=\"{game.ImageThumbnail}\">" +
                        $"<div class=\"card-body\">" +
                        $"<h4 class=\"card-title\">" +
                        $"{game.Title}" +
                        $"</h4>" +
                        $"<p class=\"card-text\">" +
                        $"<strong>" +
                        $"Price" +
                        $"</strong> " +
                        $"- {game.Price}&euro;" +
                        $"</p>" +
                        $"<p class=\"card-text\">" +
                        $"<strong>" +
                        $"Size" +
                        $"</strong>" +
                        $" - {game.Size} GB" +
                        $"</p>" +
                        $"<p class=\"card-text\">" +
                        $"{game.Description}" +
                        $"</p>" +
                        $"</div>" +
                        $"<div class=\"card-footer\">" +
                        $"<a class=\"card-button btn btn-warning\" style=\"display: {isAdmin}\" name=\"edit\" " +
                        $"href=\"/admin/edit/{game.Id}\">" +
                        $"Edit" +
                        $"</a>" +
                        $"<a class=\"card-button btn btn-danger\" style=\"display: {isAdmin}\"name=\"delete\"" +
                        $" href=\"/admin/delete/{game.Id}\">" +
                        $"Delete" +
                        $"</a>" +
                        $"<a class=\"card-button btn btn-outline-primary\" name=\"info\"" +
                        $" href=\"/game/{game.Id}\">" +
                        $"Info" +
                        $"</a>" +
                        $"<a class=\"card-button btn btn-primary\" name=\"buy\"" +
                        $" href=\"/order/{game.Id}\">" +
                        $"Buy" +
                        $"</a>" +
                        $"</div>" +
                        $"</div>");
                }

                this.ViewData["content"] = sb.ToString();
                return this.FileViewResponse(HomePath);
            }

        
    }
}
