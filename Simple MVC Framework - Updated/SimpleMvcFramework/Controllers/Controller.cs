using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SimpleMvcFramework.Contracts;
using SIS.HTTP.Requests;


namespace SimpleMvcFramework.Controllers
{
    using Views;
    using ActionResults;
    using Utilities;

    public abstract class Controller
    {
        protected Controller() { }

        public IHttpRequest Request { get; set; }

        protected IRedirectable RedirectToActrion(string redirectUrl)
        => new RedirectResult(redirectUrl);

        protected IViewable View([CallerMemberName] string caller = "")
        {
            var controllerName = ControllerUtilities.GetControllerName(this);

            var fullyQualifiedName = ControllerUtilities.GetViewFullQualifiedName(controllerName, caller);

            var view = new View(fullyQualifiedName);

            return new ViewResult(view);

        }
    }

}
