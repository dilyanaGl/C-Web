using System;
using System.Collections.Generic;
using System.Text;
using SIS.HTTP.Responses;

namespace Chushka.Controllers
{
    using Services;
    using Models;
    public class HomeController : BaseController
    {
        private readonly IChannelService channelService;
        public HomeController(IChannelService channelService)
        {
            this.channelService = channelService;
        }

        public IHttpResponse Index()
        {
         
            if(this.User != null)
            {
                var model = this.channelService.ListChannels(this.User.Username);

                return this.View<ListChannelsModel>(model);
            }
    
            return this.View();
        }
    }
}
