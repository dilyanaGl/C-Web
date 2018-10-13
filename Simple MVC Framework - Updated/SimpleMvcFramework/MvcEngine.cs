using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using SIS.WebServer;

namespace SimpleMvcFramework
{
    public static class MvcEngine
    {
        public static void Run(Server server)
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
