using WebServer.Http.Contracts;

namespace WebServer.Contracts
{
    public interface IHandeable
    {
        IHttpResponse Handle(IHttpRequest request);
    }
}
