using System.Collections.Generic;
using HTTPServer.GameWorkshop.ViewModels.Shopping;

namespace HTTPServer.GameWorkshop.Services.Contracts
{
    public interface IShoppingService
    {
        bool AddToCart(string email, int gameId);

        IEnumerable<ShoppingViewModel> ListItems(string email);
        void Remove(string email, int id);
        void Clear(string email);
    }
}