using System;
using SimpleMvc.Data;
using SimpleMvc.Framework;
using SimpleMvc.Framework.Routers;

namespace SimpleMvc.App
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var db = new NoteContext();

            using (db)
            {
                db.Database.EnsureCreated();
            }

            var server = new WebServer.WebServer(8000, new ControllerRouter());
            MvcEngine.Run(server);
        }
    }
}
