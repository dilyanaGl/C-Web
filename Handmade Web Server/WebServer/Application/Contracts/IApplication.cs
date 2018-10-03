using HTTPServer.Server.Routing.Contracts;

namespace HTTPServer.Application.Contracts
{
    public interface IApplication
    {
        void Start(IAppRouteConfig appRouteConfig);
    }
}
