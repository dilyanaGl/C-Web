﻿using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SIS.HTTP.Cookies;
using SIS.HTTP.Enums;
using System.Linq;
using System.IO;

namespace SIS.WebServer
{
    using HTTP.Common;
    using HTTP.Exceptions;
    using HTTP.Requests;
    using HTTP.Responses;
    using HTTP.Sessions;
    using Results;
     using Api;

    public class ConnectionHandler
    {
        private readonly Socket client;

        private readonly IHttpHandler handler;

        private const string RootDirectoryPath = "../../../";

        public ConnectionHandler(
            Socket client,
            IHttpHandler handler)
        {
            CoreValidator.ThrowIfNull(client, nameof(client));
            CoreValidator.ThrowIfNull(handler, nameof(handler));

            this.client = client;
            this.handler = handler;
            
        }

        private async Task<IHttpRequest> ReadRequest()
        {
            var result = new StringBuilder();
            var data = new ArraySegment<byte>(new byte[1024]);

            while (true)
            {
                int numberOfBytesRead = await this.client.ReceiveAsync(data.Array, SocketFlags.None);

                if (numberOfBytesRead == 0)
                {
                    break;
                }

                var bytesAsString = Encoding.UTF8.GetString(data.Array, 0, numberOfBytesRead);
                result.Append(bytesAsString);

                if (numberOfBytesRead < 1023)
                {
                    break;
                }
            }

            if (result.Length == 0)
            {
                return null;
            }

            return new HttpRequest(result.ToString());
        }

   
        //private IHttpResponse HandleRequestResponse(string requestPath)
        //{
        //    var extensionIndex = requestPath.LastIndexOf('.');
        //    var startResourceIndex = requestPath.LastIndexOf('/') == -1 ? 0 : requestPath.LastIndexOf('/');

        //    var resourceName = requestPath.Substring(startResourceIndex + 1);

        //    var extension = requestPath.Substring(extensionIndex + 1);

        //    var resourcePath = RootDirectoryPath + 
        //        "Resource" + 
        //        $"/{extension}" + 
        //        $"/{resourceName}";

        //    if(!File.Exists(resourcePath))
        //    {
        //        return new HttpResponse(HttpResponseStatusCode.NotFound);
        //    }

        //    var content = File.ReadAllBytes(resourcePath);
        //    return new InlineResourceResult(content, HttpResponseStatusCode.Found);            
        //}

        //private bool IsResourceRequest(IHttpRequest httpRequest)
        //{
        //    var requestPath = httpRequest.Path;
            

        //    if (!requestPath.Contains('.'))
        //    {
        //        return false;
        //    }

        //    var extension = requestPath.Substring(requestPath.LastIndexOf('.'));
        //    bool isExtensionValid = GlobalConstants.ResourceExtensions.Contains(extension);
        //    if (!isExtensionValid)
        //    {
        //        return false;
        //    }

        //    return true;


        //}

        private async Task PrepareResponse(IHttpResponse httpResponse)
        {
            byte[] byteSegments = httpResponse.GetBytes();

            await this.client.SendAsync(byteSegments, SocketFlags.None);
        }

        private string SetRequestSession(IHttpRequest httpRequest)
        {
            string sessionId = null;

            if (httpRequest.Cookies.ContainsCookie(HttpSessionStorage.SessionCookieKey))
            {
                var cookie = httpRequest.Cookies.GetCookie(HttpSessionStorage.SessionCookieKey);
                sessionId = cookie.Value;
                httpRequest.Session = HttpSessionStorage.GetSession(sessionId);
            }
            else
            {
                sessionId = Guid.NewGuid().ToString();
                httpRequest.Session = HttpSessionStorage.GetSession(sessionId);
            }

            return sessionId;
        }

        private void SetResponseSession(IHttpResponse httpResponse, string sessionId)
        {
            if (sessionId != null)
            {
                httpResponse
                    .AddCookie(new HttpCookie(HttpSessionStorage.SessionCookieKey
                        , sessionId));
            }
        }

        public async Task ProcessRequestAsync()
        {
            try
            {
                var httpRequest = await this.ReadRequest();

                if (httpRequest != null)
                {
                    string sessionId = this.SetRequestSession(httpRequest);

                    var httpResponse = this.handler.Handle(httpRequest);

                    this.SetResponseSession(httpResponse, sessionId);

                    await this.PrepareResponse(httpResponse);
                }
            }
            catch (BadRequestException e)
            {
                await this.PrepareResponse(new TextResult(e.Message, HttpResponseStatusCode.BadRequest));
            }
            catch (Exception e)
            {
                await this.PrepareResponse(new TextResult(e.Message, HttpResponseStatusCode.InternalServerError));
            }

            this.client.Shutdown(SocketShutdown.Both);
        }
    }
}