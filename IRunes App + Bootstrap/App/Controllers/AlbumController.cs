using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Controllers
{
    using SIS.HTTP.Requests;
    using SIS.HTTP.Responses;
    using Services;
    using System.Text;
    using SIS.WebServer.Results;

    public class AlbumController : BaseController
    {
        private readonly AlbumService albumService;
        public AlbumController()
        {
            this.albumService = new AlbumService();
        }

        public IHttpResponse All(IHttpRequest request)
        {
            if (!this.IsAuthenticated(request))
            {
                return new RedirectResult("/users/login");
            }

            this.SetAuthenticationDisplay(request);

            var albums = this.albumService.All();

            this.ViewData["allAlbums"] = "Currently, there are no albums.";

            if (albums.Any())
            {
                var sb = new StringBuilder();
                foreach (var album in albums)
                {

                    sb.AppendLine($"<li><strong><a href=\"/albums/details?albumId={album.Id}\">{album.Name}</a></strong></li>");

                }
                this.ViewData["allAlbums"] = sb.ToString();
            }


            return this.View("All");

        }

        public IHttpResponse Details(IHttpRequest request)
        {

            if (!this.IsAuthenticated(request))
            {
                return new RedirectResult("/users/login");
            }

            this.SetAuthenticationDisplay(request);

            string id = request.QueryData["albumId"].ToString();
            var album = this.albumService.Details(id);
            this.ViewData["Cover"] = album.Cover;
            this.ViewData["Name"] = album.Name;
            this.ViewData["Price"] = album.Price.ToString("f2");
            this.ViewData["Tracks"] = "Currently, there are no tracks.";
            this.ViewData["AlbumId"] = id;

            if (album.Tracks.Any())
            {
                var sb = new StringBuilder();
                int index = 1;
                foreach (var track in album.Tracks)
                {
                    sb.AppendLine($"<li>{index}.<a href=\"/tracks/details?trackId={track.Id}&albumId={id}\">{track.Name}</a></li>");
                    index++;
                }

                this.ViewData["Tracks"] = sb.ToString();
            }


            return this.View("AlbumDetails");

        }

        public IHttpResponse Create(IHttpRequest request)
        {
            if (!this.IsAuthenticated(request))
            {
                return new RedirectResult("/users/login");
            }
            this.SetAuthenticationDisplay(request);

            return this.View("CreateAlbum");
        }



        public IHttpResponse DoCreate(IHttpRequest request)
        {

            if (!this.IsAuthenticated(request))
            {
                return new RedirectResult("/users/login");
            }

            this.SetAuthenticationDisplay(request);
            var name = request.FormData["Name"].ToString().Replace('+', ' ');
            var cover = request.FormData["Cover"].ToString();

            this.albumService.Add(name, cover);

            return this.All(request);
        }
    }
}
