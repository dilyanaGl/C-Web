using Chushka.Models;

namespace Chushka.Services
{
    public interface IProductService
    {
        void CreateProduct(CreateProductViewModel model);
        void DeleteProduct(int id);
        ProductDetailsModel Details(int id);
        EditProductViewModel DisplayProduct(int id);
        void Edit(EditProductViewModel model);
        IndexViewModel Index();
        void Order(int id, string username);
    }
}