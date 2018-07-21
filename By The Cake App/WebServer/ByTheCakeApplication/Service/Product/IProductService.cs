using System.Collections.Generic;
using HTTPServer.ByTheCakeApplication.ViewModels.Product;

namespace HTTPServer.ByTheCakeApplication.Service.Product
{
    public interface IProductService
    {
        void Create(string name, decimal price, string imageUrl);

        IEnumerable<ProductListingViewModel> List(string searchTerm = null);

        ProductDetailsViewModel Find(int id);


        bool Exists(int id);

        IEnumerable<ProductCartViewModel> FindProducts(IEnumerable<int> ids);

    }
}
