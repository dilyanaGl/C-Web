using System;
using System.Collections.Generic;
using System.Text;
using SIS.HTTP.Responses;
using SIS.MvcFramework;

namespace Chushka.Controllers
{
    using Services;

    public class OrdersController : Controller
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public IHttpResponse All()
        {
            if (this.User.Username == null || !this.User.IsAdmin)
            {
                return this.Redirect("/");
            }

            var model = this.orderService.All();

            return this.View(model);

        }
    }
}
