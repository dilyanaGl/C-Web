using System;
using System.Collections.Generic;
using System.Text;
using SIS.MvcFramework;
using SIS.HTTP.Responses;

namespace Turshia.Controllers
{
    using Models;
    using Services;
    using Data.Models;

    public class TasksController : Controller
    {

        private readonly ITaskService taskService;

        public TasksController(ITaskService taskService)
        {
            this.taskService = taskService;

        }

        [HttpGet]
        [Authorize(nameof(Role.Admin))]
        public IHttpResponse Create()
        {

            if (this.User.Username == null)
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
        [Authorize(nameof(Role.Admin))]
        public IHttpResponse Create(CreateTaskModel model)
        {
            if (this.User.Username == null)
            {
                return this.Redirect("/");
            }

            if (!this.User.IsAdmin)
            {
                return this.Redirect("/");
            }

            this.taskService.Create(model);

            return this.Redirect("/");

        }

        [HttpGet]
        [Authorize(nameof(Role.User))]
        public IHttpResponse Details(int id)
        {
            if (this.User.Username == null)
            {
                return this.Redirect("/");
            }

        
            var model = this.taskService.Details(id);

            if (model == null)
            {
                return this.Redirect("/");

            }
            return this.View(model);

        }

        [Authorize(nameof(Role.User))]
        [HttpGet]
        public IHttpResponse Report(int id)
        {
            if (this.User.Username == null)
            {
                return this.Redirect("/");
            }

            this.taskService.Report(id, this.User.Username);
            return this.Redirect("/");

        }
    }
}
