using System;
using SIS.WebServer;
namespace Demo
{
    
     using SimpleMvcFramework.Routes;
     using SimpleMvcFramework;

    class Program
    {
        static void Main(string[] args)
        {
            var router = new ControllerRouter();
           
            var server = new Server(8000, router);

            MvcEngine.Run(server);
        }
    }
}
