using System;

namespace SIS.Demo
{
    using WebServer;
    using WebServer.Routing;
    using Http.Enums;
    class Launcher
    {
        static void Main(string[] args)
        {
            ServerRoutingTable serverRoutingTable = new ServerRoutingTable();

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/"]
                = request => new HomeController().Index();

            Server server = new Server(7777, serverRoutingTable);
            server.Run();
        }
    }
}
