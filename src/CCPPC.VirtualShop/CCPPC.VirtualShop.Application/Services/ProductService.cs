using CCPPC.VirtualShop.Application.Interfaces;
using CCPPC.VirtualShop.Application.Models;
using CCPPC.VirtualShop.Domain.Entities;
using CCPPC.VirtualShop.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CCPPC.VirtualShop.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public Task<Product> Get(long productId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public async Task<Product> Insert(ProductViewModel model)
        {
            Product product = await _repository.Insert(new Product(model.Name, model.Description, model.Quantity, model.Value));
            return product;
        }

        public Task<Product> Update(long productId, ProductViewModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}
