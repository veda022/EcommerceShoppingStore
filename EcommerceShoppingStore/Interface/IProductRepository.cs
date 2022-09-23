using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceShoppingStore.Models;

namespace EcommerceShoppingStore.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<Category>> GetCategories();

        Task<IEnumerable<Product>> GetProducts();

        Task<ProductViewModel> GetProduct(int? ProductId);

        Task<int> AddCategory(Category cat);

        Task<int> AddProduct(Product prod);

        //Task<int> DeleteCategory(int? CategoryId);

        Task<int> DeleteProduct(int? ProductId);

        Task UpdateProduct(Product prod);
    }
}
