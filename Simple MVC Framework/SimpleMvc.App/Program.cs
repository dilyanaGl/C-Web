using System;
using SimpleMvcFramework;
using SimpleMvcFramework.Routes;
using Microsoft.EntityFrameworkCore;

namespace SimpleMvc.App
{
    using Data;

    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new NoteDbContext())
            {
                db.Database.Migrate();
            }
            MvcEngine.Run(new WebServer.WebServer(8888, new ControllerRouter()));
        }
    }
}
