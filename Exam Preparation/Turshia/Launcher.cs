using System;
using SIS.MvcFramework;
using SIS.WebServer;
using SIS.MvcFramework.Routing;
using Microsoft.EntityFrameworkCore;

namespace Turshia
{
    using Data;

    class Launcher
    {
        static void Main(string[] args)
        {
            WebHost.Start(new StartUp());
        }

        //private void ConfigureServices(IDependencyContainer container)
        //{
        //    container.RegisterDependency<IUserService, UserService>();
        //    container.RegisterDependency<IHashService, HashService>();
        //}


    }
}
