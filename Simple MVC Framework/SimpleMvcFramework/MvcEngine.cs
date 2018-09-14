using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace SimpleMvcFramework
{
    public static class MvcEngine
    {
        public static void Run(WebServer.WebServer server)
        {
            RegisterAssemblyName();
           

                try
                {
                    server.Run();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message} {ex.InnerException}");
                }
        }

        private static void RegisterAssemblyName()
        {
            MvcContext.Get.AssemblyName = Assembly.GetEntryAssembly().GetName().Name;

        }
    }
}
