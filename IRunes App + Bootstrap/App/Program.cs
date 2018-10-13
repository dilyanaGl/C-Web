using System;

namespace App
{
    using SIS.WebServer;
    using SIS.WebServer.Routing;
    using SIS.HTTP.Enums;
    using Controllers;

    class Program
    {
        static void Main(string[] args)
        {
            ServerRoutingTable serverRoutingTable = new ServerRoutingTable();

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/"] 
                = request => new HomeController().Index(request);

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/home/index"]
              = request => new HomeController().Index(request);

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/users/register"] = 
                request => new UsersController().Register(request);

            serverRoutingTable.Routes[HttpRequestMethod.Post]["/users/doRegister"] =
                request => new UsersController().DoRegister(request);

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/users/login"] = 
                request => new UsersController().Login(request);

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/users/logout"] =
                request => new UsersController().Logout(request);

            serverRoutingTable.Routes[HttpRequestMethod.Post]["/users/doLogin"] =
                request => new UsersController().DoLogin(request);

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/albums/create"]
                = request => new AlbumController().Create(request);

            serverRoutingTable.Routes[HttpRequestMethod.Post]["/albums/create"]
               = request => new AlbumController().DoCreate(request);

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/albums/all"] =
                request => new AlbumController().All(request);  

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/albums/details"] =
               request => new AlbumController().Details(request);

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/tracks/details"] =
                request => new TrackController().Details(request);

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/tracks/create"] = 
                request => new TrackController().Create(request);

            serverRoutingTable.Routes[HttpRequestMethod.Post]["/tracks/create"] = 
                request => new TrackController().DoCreate(request);





            // serverRoutingTable.Routes[HttpRequestMethod.Post][?] = request => new AccountController().DoLogin(request);

            Server server = new Server(7777, serverRoutingTable);
            server.Run();
        }
    }
}
