using System;
using System.Collections.Generic;
using System.Text;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using System.IO;
using System.Linq;

namespace SIS.WebServer.Api
{
    using Routing;
    using HTTP.Enums;
    using HTTP.Common;
    using Results;

    public class HttpHandler : IHttpHandler
    {
        private readonly ServerRoutingTable serverRoutingTable;
        private const string RootDirectoryPath = "../../../";

        public HttpHandler(ServerRoutingTable routingTable)
        {
            this.serverRoutingTable = routingTable;
        }

        public IHttpResponse Handle(IHttpRequest httpRequest)
        {
            var isResourceRequest = this.IsResourceRequest(httpRequest);

            if (isResourceRequest)
            {
                return this.HandleRequestResponse(httpRequest.Path);
            }

            if (!this.serverRoutingTable.Routes.ContainsKey(httpRequest.RequestMethod)
                         || !this.serverRoutingTable.Routes[httpRequest.RequestMethod].ContainsKey(httpRequest.Path))
            {
                return new HttpResponse(HttpResponseStatusCode.NotFound);
            }


            return this.serverRoutingTable.Routes[httpRequest.RequestMethod][httpRequest.Path].Invoke(httpRequest);
        }

        private IHttpResponse HandleRequestResponse(string requestPath)
        {
            var extensionIndex = requestPath.LastIndexOf('.');
            var startResourceIndex = requestPath.LastIndexOf('/') == -1 ? 0 : requestPath.LastIndexOf('/');

            var resourceName = requestPath.Substring(startResourceIndex + 1);

            var extension = requestPath.Substring(extensionIndex + 1);

            var resourcePath = RootDirectoryPath +
                "Resource" +
                $"/{extension}" +
                $"/{resourceName}";

            if (!File.Exists(resourcePath))
            {
                return new HttpResponse(HttpResponseStatusCode.NotFound);
            }

            var content = File.ReadAllBytes(resourcePath);
            return new InlineResourceResult(content, HttpResponseStatusCode.Found);
        }

        private bool IsResourceRequest(IHttpRequest httpRequest)
        {
            var requestPath = httpRequest.Path;


            if (!requestPath.Contains('.'))
            {
                return false;
            }

            var extension = requestPath.Substring(requestPath.LastIndexOf('.'));
            bool isExtensionValid = GlobalConstants.ResourceExtensions.Contains(extension);
            if (!isExtensionValid)
            {
                return false;
            }

            return true;


        }


    }
}
