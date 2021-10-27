using CCPPC.VirtualShop.Data.Context;
using CCPPC.VirtualShop.Domain.Entities;
using CCPPC.VirtualShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CCPPC.VirtualShop.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<Product> Get(long productId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAdd()
        {
            throw new NotImplementedException();
        }

        public async Task<Product> Insert(Product product)
        {
            var insertResult = await _context.Products.AddAsync(product);
            return insertResult.Entity;
        }

        public Task<Product> Update(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
