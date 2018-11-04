using System;
using System.Collections.Generic;
using System.Text;
using SIS.MvcFramework;
namespace Chushka.Controllers
{
    using Services;
    using Models;
    using SIS.HTTP.Exceptions;
    using SIS.HTTP.Responses;

    public class ChannelsController : Controller
    {
        private readonly IChannelService channelService;

        public ChannelsController(IChannelService channelService)
        {
            this.channelService = channelService;

        }

        [Authorize]
        public IHttpResponse Create()
        {
            if (!this.Request.Cookies.ContainsCookie("auth"))
            {
                return this.Redirect("/");
            }

            if (!this.User.IsAdmin)
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IHttpResponse Create(CreateChannelModel model)
        {
            if (!this.Request.Cookies.ContainsCookie("auth"))
            {
                return this.Redirect("/");
            }

            if (!this.User.IsAdmin)
            {
                return this.Redirect("/");
            }

            this.channelService.CreateChannel(model);
            return this.Redirect("/");

        }

        [Authorize]
        [HttpGet]
        public  IHttpResponse Details(int id)
        {
            if (!this.Request.Cookies.ContainsCookie("auth"))
            {
                return this.Redirect("/");
            }
            var model = this.channelService.Details(id);
            return this.View<ChannelDetailsModel>(model);
        }

        [HttpGet]
        [Authorize]
        public IHttpResponse Follow(int id)
        {
            if (!this.Request.Cookies.ContainsCookie("auth"))
            {
                return this.Redirect("/");
            }

            bool success = this.channelService.Follow(id, this.User.Username);

            if(!success)
            {
                return this.BadRequestErrorWithView("Operation not allowed.");
            }
            return this.Redirect("/");
        }

        [HttpGet]
        [Authorize]
        public IHttpResponse Followed()
        {
            if (!this.Request.Cookies.ContainsCookie("auth"))
            {
                return this.Redirect("/");
            }

            var model = this.channelService.SeeFollowing(this.User.Username);
            return this.View<FollowedChannelsModel>(model);
        }

        [HttpGet]
        [Authorize]
        public IHttpResponse Unfollow(int id)
        {
            if (!this.Request.Cookies.ContainsCookie("auth"))
            {
                return this.Redirect("/");
            }

       
            this.channelService.Unfollow(id, this.User.Username);

            return this.Redirect("/Channels/Followed");
        }
    }
}
