using System;
using System.Collections.Generic;
using System.Text;
using SIS.MvcFramework.Services;
using Turshia.Data;
using Microsoft.EntityFrameworkCore;


namespace Turshia
{
    using Services;
    using SIS.MvcFramework;

    public class StartUp : IMvcApplication
    { 

        public void ConfigureServices(IServiceCollection container)
        {
            container.AddService<IUserService, UserService>();
            container.AddService<IHashService, HashService>();
            container.AddService<ITaskService, TaskService>();
            container.AddService<IReportService, ReportService>();
            
        }

        MvcFrameworkSettings IMvcApplication.Configure()
        {
            var db = new TurshiaDbContext();
            db.Database.Migrate();
            return new MvcFrameworkSettings();
        }
    }
}
