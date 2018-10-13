using System;
using System.Collections.Generic;
using System.Text;
using SIS.HTTP.Requests;

namespace App.Controllers
{
    using SIS.HTTP.Responses;
    using Services;
    using SIS.WebServer.Results;

    public class TrackController : BaseController
    {
        private readonly TrackService trackService;

        public TrackController()
        {
            this.trackService = new TrackService();
        }

        public IHttpResponse Create(IHttpRequest request)
        {
            if (!this.IsAuthenticated(request))
            {
                return new RedirectResult("/users/login");
            }

            this.SetAuthenticationDisplay(request);
            string albumId = request.QueryData["albumId"].ToString();
            this.ViewData["AlbumId"] = albumId;
            return this.View("CreateTrack");
        }


        public IHttpResponse DoCreate(IHttpRequest request)
        {
            if (!this.IsAuthenticated(request))
            {
                return new RedirectResult("/users/login");
            }
            this.SetAuthenticationDisplay(request);

            string albumId = request.FormData["AlbumId"].ToString();

            var name = request.FormData["Name"].ToString().Replace('+', ' ');
            var link = request.FormData["Link"].ToString();
            decimal price = decimal.Parse(request.FormData["Price"].ToString());
            var track = this.trackService.Create(name, link, price, albumId);
            var id = this.trackService.GetId(name, albumId);
            return this.Details(request);
        }

        public IHttpResponse Details(IHttpRequest request)
        {
            if (!this.IsAuthenticated(request))
            {
                return new RedirectResult("/users/login");
            }

            this.SetAuthenticationDisplay(request);

            string trackId = request.QueryData["trackId"].ToString();
            string albumId = request.QueryData["albumId"].ToString();
            var track = this.trackService.Details(trackId, albumId);

            this.ViewData["Name"] = track.Name;
            this.ViewData["Price"] = track.Price.ToString();
            this.ViewData["AlbumId"] = track.AlbumId;

            return this.View("TrackDetails");
        }

         
    }
}
