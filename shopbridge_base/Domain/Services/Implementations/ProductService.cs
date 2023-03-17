using Microsoft.Extensions.Logging;
using Shopbridge_base.Data.Repository;
using Shopbridge_base.Domain.Models;
using Shopbridge_base.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopbridge_base.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> logger;
        private readonly IRepository<Product> repository;

        public ProductService(IRepository<Product> _repository, ILogger<ProductService> _logger)
        {
            this.repository = _repository;
            this.logger = _logger;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await repository.GetAllAsync();
        }

        public async Task<Product> GetProduct(int id)
        {
            return await repository.GetById(id);
        }

        public async Task<bool> FindProduct(int id)
        {
            var product = await repository.GetById(id);
            return (product != null ? true : false);
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            await repository.AddAsync(product);
            return product;
        }

        public async Task UpdateProductAsync(int productId, Product product)
        {
            Product entityToUpdate = repository.GetById(productId).Result;
            if(entityToUpdate != null)
            {
                entityToUpdate.Price = product.Price;
                entityToUpdate.Description = product.Description;
                entityToUpdate.ModificationDate = DateTime.Now;
                entityToUpdate.Name = product.Name;

                await repository.UpdateAsync(entityToUpdate);
            }
            
        }

        public async Task DeleteProductAsync(int productId)
        {
            await repository.DeleteAsync(productId);
        }


    }
}
