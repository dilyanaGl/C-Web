using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SimpleMvcFramework.Attributes;
using SimpleMvcFramework.Contracts;
using SimpleMvcFramework.Controllers;
using SIS.WebServer.Api;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.HTTP.Enums;
using SIS.WebServer.Results;

namespace SimpleMvcFramework.Routes
{
    using Utilities;

    public class ControllerRouter : IHttpHandler
    {
           private Controller GetController(string controllerName, IHttpRequest request)
        {
            if (controllerName != null)
            {
                //string controllerTypeName = string.Format(
                //"{0}.{1}.{2}, {0}",
                //MvcContext.Get.AssemblyName,
                //MvcContext.Get.ControllersFolder,
                //controllerName);

                //var controllerType = Type.GetType(controllerTypeName);

                var controllerType = Assembly.GetEntryAssembly()
                    .GetTypes()
                    .Where(p => p.Name == controllerName)
                    .FirstOrDefault();

                var controller = (Controller)Activator.CreateInstance(controllerType);

                if (controller != null)
                {
                    controller.Request = request;
                }

                return controller;
            }

            return null;
        }

        private MethodInfo GetMethod(string requestMethod, Controller controller, string actionName)
        {
            MethodInfo method = null;

            foreach (var methodInfo in GetSuitableMethods(controller, actionName))
            {
                var attributes = methodInfo
                .GetCustomAttributes()
                .Where(p => p is HttpMethodAttribute)
                .Cast<HttpMethodAttribute>();


                if (!attributes.Any() && requestMethod.ToUpper() == "GET")
                {
                    return methodInfo;
                }

                foreach (var attribute in attributes)
                {

                    if (attribute.IsValid(requestMethod))
                    {
                        return methodInfo;
                    }
                }

                return method;

            }
            return null;
        }

        private IEnumerable<MethodInfo> GetSuitableMethods(Controller controller, string action)
        {
            if (controller == null)
            {
                return new MethodInfo[0];
            }

            return controller
            .GetType()
            .GetMethods()
            .Where(methodInfo => methodInfo.Name.ToLower() == action.ToLower());

        }

        private IHttpResponse PrepareResponse(Controller controller, MethodInfo action)
        {
            IActionResult actionResult = (IActionResult)action.Invoke(controller, null);

            string invocationResult = actionResult.Invoke();

            if (actionResult is IViewable)
            {
                return new HtmlResult(invocationResult, HttpResponseStatusCode.Ok);
            }


            else if (actionResult is IRedirectable)
            {
                return new RedirectResult(invocationResult);
            }

            else
            {
                throw new InvalidOperationException("The view is not supported");
            }

        }

        public IHttpResponse Handle(IHttpRequest request)
        {
            
            string controllerName = "HomeController";
            string actionName = "Index";
            if(request.Path != "/")
            {
              var pathParts = request.Path.Split('/');
                if(pathParts.Length != 3)
                {
                    return new HttpResponse(HttpResponseStatusCode.BadRequest);
                }

                controllerName = String.Concat(ControllerUtilities.CapitalizeFirstLetter(pathParts[1]), MvcContext.Get.ControllersSuffix);

                actionName = ControllerUtilities.CapitalizeFirstLetter(pathParts[2]);
            }
         
            var controller = this.GetController(controllerName, request);           
            var action = this.GetMethod(request.RequestMethod.ToString(), controller, actionName);

            if (action == null)
            {
                return new HttpResponse(HttpResponseStatusCode.NotFound);
            }

            return this.PrepareResponse(controller, action);

        }
    }
}

