using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SimpleMvc.Framework
{
    public static class MvcEngine
    {
        public static void Run(WebServer.WebServer server)
        {
            RegisterAssemblyName();
            RegisterControllersData();
            RegisterViewsData();
            RegisterModelsData();

            try
            {
                server.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.InnerException);
            }
        }

        private static void RegisterModelsData()
        {
            MvcContext.Get.ModelsFolder = "Models";
        }

        private static void RegisterViewsData()
        {
            MvcContext.Get.ViewsFolder = "Views";
        }

        private static void RegisterControllersData()
        {
            MvcContext.Get.ControllersFolder = "Controllers";
            MvcContext.Get.ControllersSuffix = "Controller";
        }

        private static void RegisterAssemblyName()
        {
            MvcContext.Get.AssemblyName = Assembly
                .GetEntryAssembly()
                .GetName()
                .Name;
        }
    }
}
