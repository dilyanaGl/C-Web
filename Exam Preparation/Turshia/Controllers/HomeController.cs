using System;
using System.Collections.Generic;
using System.Text;
using SIS.HTTP.Responses;

namespace Turshia.Controllers
{
    using Services;
    using Models;
  
    public class HomeController : BaseController
    {
        private readonly ITaskService taskService;
        public HomeController(ITaskService channelService)
        {
            this.taskService = channelService;
        }

        public IHttpResponse Index()
        {
         
            if(this.User.Username != null)
            {
                var model = this.taskService.All();

                return this.View<IndexViewModel>("LoggedInUser", model);
            }
    
            return this.View();
        }
    }
}
