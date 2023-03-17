using Shopbridge_base.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopbridge_base.Domain.Services.Interfaces
{
    public interface IProductService
    {
        Task<Product> AddProductAsync(Product product);
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProduct(int id);
        Task<bool> FindProduct(int id);
        Task UpdateProductAsync(int productId, Product product);
        Task DeleteProductAsync(int productId);
    }
}
