using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using SimpleMvcFramework.Contracts;
using SimpleMvcFramework.Contracts.Generics;
using SimpleMvcFramework.Helpers;
using SimpleMvcFramework.ViewEngine;
using SimpleMvcFramework.ViewEngine.Generic;

namespace SimpleMvcFramework.Controllers
{
    public abstract class Controller
    {
        protected IActionResult View([CallerMemberName] string caller="")
        {
            string controllername = ControllerHelpers.GetControllerName(this);

            string fullQualifiedName = ControllerHelpers.GetFullQualifiedName(controllername, caller);

            return new ActionResult(fullQualifiedName);
        }

        protected IActionResult<TModel> View<TModel>(TModel model, [CallerMemberName] string caller="")
        {
            string controllerName = ControllerHelpers.GetControllerName(this);
            string fullQualifiedName = ControllerHelpers.GetFullQualifiedName(controllerName, caller);

            return new ActionResult<TModel>(fullQualifiedName, model); 

        }

        protected IActionResult View(string controller, string action)
        {
            var fullQualifiedName = ControllerHelpers.GetFullQualifiedName(controller, action);

            return new ActionResult(fullQualifiedName);
        }

        protected IActionResult<TModel> View<TModel>(TModel model, string controller, string action)
        {
            var fullQualifiedName = ControllerHelpers
                .GetFullQualifiedName(controller, action);

            return new ActionResult<TModel>(fullQualifiedName, model);
        }
    }
}
