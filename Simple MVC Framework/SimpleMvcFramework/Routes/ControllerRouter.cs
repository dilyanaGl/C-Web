using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SimpleMvcFramework.Attributes;
using SimpleMvcFramework.Contracts;
using SimpleMvcFramework.Controllers;
using SimpleMvcFramework.Helpers;
using WebServer.Contracts;
using WebServer.Enums;
using WebServer.Exceptions;
using WebServer.Http.Contracts;
using WebServer.Http.Response;

namespace SimpleMvcFramework.Routes
{
    public class ControllerRouter : IHandeable
    {
        private IDictionary<string, string> getParams;
        private IDictionary<string, string> postParams;
        private string controllerName;
        private string actionName;
        private string requestMethod;
        private object[] methodParams;

        public IHttpResponse Handle(IHttpRequest request)
        {
            if(request.Path == "/favicon.ico")
            {
                return null; 
            }

            this.getParams = new Dictionary<string, string>(request.UrlParameters);
            this.postParams = new Dictionary<string, string>(request.FormData);
            this.requestMethod = request.Method.ToString().ToUpper();
            this.PrepareControllerAndActionNames(request.Path);
            var controller = this.GetController();
            var method = this.GetMethod();
            if (method == null)
            {
                return new NotFoundResponse();
            }
            this.PrepareMethodParameters(method);

            IInvocable actionResult = (IInvocable) method.Invoke(controller, this.methodParams);

            string content = actionResult.Invoke();

            IHttpResponse response = new ContentResponse(HttpStatusCode.Ok, content);

            return response;
        }

        private void PrepareMethodParameters(MethodInfo method)
        {
            var parameters = method.GetParameters();

            this.methodParams = new object[parameters.Length];
            int index = 0;

            foreach (var parameterInfo in parameters)
            {
                if (parameterInfo.ParameterType.IsPrimitive || parameterInfo.ParameterType == typeof(String))
                {
                    var value = this.getParams[parameterInfo.Name];

                    var convertValue = Convert.ChangeType(value, parameterInfo.ParameterType);

                    this.methodParams[index] = convertValue;
                    index++;
                }
                else
                {
                    var modelType = parameterInfo.ParameterType;

                    var modelInstance = Activator.CreateInstance(modelType);

                    var properties = modelType.GetProperties();
                    

                    foreach (var propertyInfo in properties)
                    {
                        var parameterValue = this.postParams[propertyInfo.Name];

                        var convertedValue = Convert.ChangeType(parameterValue, propertyInfo.PropertyType); 

                        propertyInfo.SetValue(modelInstance, convertedValue);
                    }

                    this.methodParams[index] = modelInstance;
                    index++;
                }
            }
        }


        private MethodInfo GetMethod()
        {
           foreach (var method in this.GetSuitableMethods())
            {
                var httpMethodAttributes = method
                    .GetCustomAttributes()
                    .Where(p => p is HttpMethodAttribute)
                    .Cast<HttpMethodAttribute>();

                if (!httpMethodAttributes.Any() && this.requestMethod == "GET")
                {
                    return method;
                }

                foreach (var httpAttribute in httpMethodAttributes)
                {
                    if (httpAttribute.IsValid(requestMethod))
                    {
                        return method;
                    }
                }

            }

            return null;

        }

        private IEnumerable<MethodInfo> GetSuitableMethods()
        {
            var controller = this.GetController();

            if (controller == null)
            {
                return new MethodInfo[0];
            }

            var methods = controller
                .GetType()
                .GetMethods()
                .Where(
                    p => p.Name == this.actionName);

            return methods;
        }

        private Controller GetController()
        {
            var fullQualifiedName = String.Format("{0}.{1}.{2}, {0}",
                Assembly.GetEntryAssembly().GetName().Name,
                MvcContext.Get.ControllersFolder,
                controllerName);

            var controllerType = Type.GetType(fullQualifiedName);

            if (controllerType == null)
            {
                return null;
            }

            return (Controller)Activator.CreateInstance(controllerType);
        }

        private void PrepareControllerAndActionNames(string path)
        {
            if(path == "/")
            {
                path = "home/index";
            }
            var paths = path.Split(new[] { '/', '?' }, StringSplitOptions.RemoveEmptyEntries);

            if (paths.Length < 2)
            {
                BadRequestException.ThrowFromInvalidRequest();
            }

            this.controllerName = $"{StringExtensions.CapitalizeFirstLetter(paths[0])}{MvcContext.Get.ControllersSuffix}";

            this.actionName = StringExtensions.CapitalizeFirstLetter(paths[1]);




        }
    }
}

