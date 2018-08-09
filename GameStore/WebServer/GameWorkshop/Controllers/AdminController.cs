using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;
using HTTPServer.GameWorkshop.Infrastructure;
using HTTPServer.GameWorkshop.Services;
using HTTPServer.GameWorkshop.Services.Contracts;
using HTTPServer.GameWorkshop.ViewModels.Account;
using HTTPServer.GameWorkshop.ViewModels.Admin;
using HTTPServer.Server.Http.Contracts;

namespace HTTPServer.GameWorkshop.Controllers
{
    public class AdminController : Controller
    {
        private const string AddGamePath = @"Admin\AddGame";
        private const string ListGamesPath = @"Admin\AllGames";
        private const string EditGamesPath = @"Admin\EditGame";
        private const string DeleteGamesPath = @"Admin\DeleteGame";
        
        private readonly IGameService gameService;

        public AdminController(IHttpRequest request) : base(request)
        {
            this.gameService = new GameService();
        }

        public IHttpResponse Add()
        {
            return PerformAdminCheck(AddGamePath);
        }

        public IHttpResponse Add(AddGameViewModel model)
        {
            var error = ValidateModel(model);

            if (error != null)
            {
                this.AddError(error);
            }

            var success = this.gameService.AddGame(model.Title, model.ImageThumbnail, model.Size, model.Price,
                model.YouTubeVideoId, model.Description, model.ReleaseDate);

            return this.FileViewResponse(ListGamesPath);


        }

        public IHttpResponse List()
        {
            var isAdmin = this.Authentication.IsAdmin;
            if (isAdmin)
            {
                var games = gameService.List();
                var sb = new StringBuilder();

                var @class = "class=\"table-success\"";
                int rowCount = 1;

                foreach (var game in games)
                {
                    sb.AppendLine(String.Format(
                        "<tr {0}>" +
                        "<th scope = \"row\" > {1} </ th > " +
                        "<td> {2} </td> " +
                        "<td> {3} GB</td>" +
                        "<td> {4} &euro;</ td >" +
                        "<td>" +
                        "<a href = \"{5}{6}\" class=\"btn btn-warning btn-sm\">Edit</a>" +
                        "<a href = \"{7}{6}\" class=\"btn btn-danger btn-sm\">Delete</a>" +
                        "</td>" +
                        "</tr>",
                        rowCount % 2 == 1 ? @class : "",
                        rowCount,
                        game.Name,
                        game.Size,
                        game.Price,
                        "/admin/edit/",
                        game.Id,
                        "/admin/delete/"

                    ));

                    rowCount++;
                }

                this.ViewData["content"] = sb.ToString().Trim();

                return this.FileViewResponse(ListGamesPath);
            }

            else
            {
                return RedirectResponse("/");
            }
        }

        public IHttpResponse Edit()
        {
            var isAdmin = this.Authentication.IsAdmin;

            if (isAdmin)
            {
                var game = gameService.FetchGame(int.Parse(this.Request.UrlParameters["id"]));
                this.ViewData["name"] = game.Title;
                this.ViewData["description"] = game.Description;
                this.ViewData["url"] = game.ImageThumbnail;
                this.ViewData["video"] = game.YouTubeVideoId;
                this.ViewData["size"] = game.Size.ToString();
                this.ViewData["price"] = game.Price.ToString();
                this.ViewData["year"] = game.ReleaseDate.Year.ToString();
                this.ViewData["month"] = game.ReleaseDate.Month.ToString("d2");
                this.ViewData["day"] = game.ReleaseDate.Day.ToString("d2");

                return this.FileViewResponse(EditGamesPath);
            }

            else
            {
                return RedirectResponse("/");
            }

        }

        public IHttpResponse Edit(EditGamesViewModel model)
        {
            var success = this.gameService.EditGame(model.Id, model.Title, model.ImageThumbnail, model.Size,
                model.Price,
                model.YouTubeVideoId, model.Description, model.ReleaseDate);

            if (!success)
            {
                this.AddError("Invalid details!");
                return RedirectResponse("/admin/edit/{(?<id>[0 - 9]+)}");
            }

            return RedirectResponse("/admin/list");


        }

        public IHttpResponse Delete(int id)
        {
            var success = this.gameService.DeleteGame(id);

            if (!success)
            {
                return RedirectResponse($"/admin/delete/{id}");
            }

            return RedirectResponse("/admin/list");
        }

        public IHttpResponse Delete()
        {
            var isAdmin = this.Authentication.IsAdmin;

            if (isAdmin)
            {
                var game = gameService.FetchGame(int.Parse(this.Request.UrlParameters["id"]));
                this.ViewData["name"] = game.Title;
                this.ViewData["description"] = game.Description;
                this.ViewData["url"] = game.ImageThumbnail;
                this.ViewData["video"] = game.YouTubeVideoId;
                this.ViewData["size"] = game.Size.ToString();
                this.ViewData["price"] = game.Price.ToString();
                this.ViewData["year"] = game.ReleaseDate.Year.ToString();
                this.ViewData["month"] = game.ReleaseDate.Month.ToString("d2");
                this.ViewData["day"] = game.ReleaseDate.Day.ToString("d2");

                return this.FileViewResponse(DeleteGamesPath);
            }

            else
            {
                return RedirectResponse("/");
            }
        }

        private string ValidateModel(object model)
        {
            var context = new ValidationContext(model);
            var validationResults = new List<ValidationResult>();

            if (Validator.TryValidateObject(model, context, validationResults, true) == false)
            {
                foreach (var result in validationResults)
                {
                    if (result != ValidationResult.Success)
                    {
                        return result.ErrorMessage;
                    }
                }
            }

            return null;

        }

        private IHttpResponse PerformAdminCheck(string path)
        {
            var isAdmin = this.Authentication.IsAdmin;

            if (isAdmin)
            {
                return this.FileViewResponse(path);
            }

            else
            {
                return RedirectResponse("/");
            }
        }
    }

}
    


    

