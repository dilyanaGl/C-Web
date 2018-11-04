using System;
using System.Collections.Generic;
using System.Text;
using SIS.MvcFramework.Services;
using Chushka.Data;
using Microsoft.EntityFrameworkCore;


namespace Chushka
{
    using Services;
    using SIS.MvcFramework;

    public class StartUp : IMvcApplication
    { 

        public void ConfigureServices(IServiceCollection container)
        {
            container.AddService<IUserService, UserService>();
            container.AddService<IHashService, HashService>();
            container.AddService<IChannelService, ChannelService>();
        }

        MvcFrameworkSettings IMvcApplication.Configure()
        {
            //var db = new MishMashDbContext();
            //db.Database.Migrate();
            return new MvcFrameworkSettings();
        }
    }
}
