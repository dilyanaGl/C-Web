using Chushka.Models;

namespace Chushka.Services
{
    public interface IOrderService
    {
        AllOrdersViewModel All();
        bool Order(int productId, string username);
    }
}