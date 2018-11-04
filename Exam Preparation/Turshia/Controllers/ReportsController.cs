using System;
using System.Collections.Generic;
using System.Text;
using SIS.MvcFramework;
using SIS.HTTP.Responses;


namespace Turshia.Controllers
{
    using Data.Models;
    using Services;

    public class ReportsController : Controller
    {
        private readonly IReportService reportService;

        public ReportsController(IReportService reportService)
        {

            this.reportService = reportService;

        }


        [Authorize(nameof(Role.Admin))]
        [HttpGet]
        public IHttpResponse All()
        {
            if (this.User.Username == null)
            {
                return this.Redirect("/");
            }

            if (!this.User.IsAdmin)
            {
                return this.Redirect("/");
            }
            var model = this.reportService.List();
            return this.View(model);


        }

        [Authorize(nameof(Role.Admin))]
        public IHttpResponse Details(int id)
        {
            if (this.User.Username == null)
            {
                return this.Redirect("/");
            }

            if (!this.User.IsAdmin)
            {
                return this.Redirect("/");
            }

            var model = this.reportService.Details(id);
            return this.View(model);
        }
    }
}
