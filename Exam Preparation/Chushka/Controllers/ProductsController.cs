using System;
using System.Collections.Generic;
using System.Text;
using SIS.MvcFramework;
using SIS.HTTP.Responses;

namespace Chushka.Controllers
{
    using Services;
    using Models;
    using Data.Models;

    public class ProductsController : Controller
    {
        private readonly IProductService productService;
        public ProductsController(IProductService productService)
        {
            this.productService = productService;

        }

        public IHttpResponse Details(int id)
        {
            if(this.User.Username == null)
            {
                return this.Redirect("/");
            }
            var model = this.productService.Details(id);
            return this.View(model);

        }

        [HttpGet]
        [Authorize(nameof(Role.Admin))]
        public IHttpResponse Create()
        {
            //if (!this.User.IsAdmin)
            //{
            //    return this.Redirect("/");
            //}
            return this.View();
        }

        [HttpPost]
        public IHttpResponse Create(CreateProductViewModel model)
        {
            if (!this.User.IsAdmin)
            {
                return this.Redirect("/");
            }

            this.productService.CreateProduct(model);

            return this.Redirect("/");
        }

        [HttpGet]
        public IHttpResponse Edit(int id)
        {
            if (!this.User.IsAdmin)
            {
                return this.Redirect("/");
            }
            var model = this.productService.DisplayProduct(id);
            return this.View(model);
        }

        [HttpPost]
        public IHttpResponse Edit(EditProductViewModel model)
        {
            if (!this.User.IsAdmin)
            {
                return this.Redirect("/");
            }
            this.productService.Edit(model);
            return this.Redirect("/");

        }

        [HttpGet]
        public IHttpResponse Delete(int id)
        {
            if (!this.User.IsAdmin)
            {
                return this.Redirect("/");
            }

            var model = this.productService.DisplayProduct(id);

            return this.View(model);

        }

        [HttpGet]
        public IHttpResponse Order(int id)
        {
            if (this.User.Username == null)
            {
                return this.Redirect("/");
            }
            this.productService.Order(id, this.User.Username);
            return this.Redirect("/");
        }


        [HttpPost]
        public IHttpResponse Delete(EditProductViewModel model)
        {
            if (!this.User.IsAdmin)
            {
                return this.Redirect("/");
            }

            productService.DeleteProduct(model.Id);
            return this.Redirect("/");

        }
    }
}
