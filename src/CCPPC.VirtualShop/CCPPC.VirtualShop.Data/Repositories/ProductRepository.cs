using CCPPC.VirtualShop.Data.Context;
using CCPPC.VirtualShop.Domain.Entities;
using CCPPC.VirtualShop.Domain.Enums;
using CCPPC.VirtualShop.Domain.Interfaces;
using CCPPC.VirtualShop.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCPPC.VirtualShop.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly LogModel _log;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
            _log = new LogModel();
        }

        public async Task<Product> Get(long productId)
        {
            try
            {
                return await _context.Products.FindAsync(productId);
            }
            catch (Exception ex)
            {
                _log.Record(nameof(Get),
                    $"Exception: [Message:{ex.Message}\nInnerException:{ex.InnerException}\nStackTrace:{ex.StackTrace}]",
                    LogType.Error);
                throw;
            }
        }
        public async Task<Product> GetByName(string name)
        {
            try
            {
                return await _context.Products.SingleOrDefaultAsync(x => x.Name.ToUpper() == name.ToUpper());
            }
            catch (Exception ex)
            {
                _log.Record(nameof(Get),
                    $"Exception: [Message:{ex.Message}\nInnerException:{ex.InnerException}\nStackTrace:{ex.StackTrace}]",
                    LogType.Error);
                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            try
            {
                return _context.Products.AsEnumerable();
            }
            catch (Exception ex)
            {
                _log.Record(nameof(GetAll),
                    $"Exception: [Message:{ex.Message}\nInnerException:{ex.InnerException}\nStackTrace:{ex.StackTrace}]",
                    LogType.Error);
                throw;
            }
        }

        public async Task<Product> Insert(Product product)
        {
            try
            {
                var insertResult = await _context.Products.AddAsync(product);
                if (await _context.Commit() > 0)
                    return insertResult.Entity;
                return default;
            }
            catch (Exception ex)
            {
                _log.Record(nameof(Insert),
                    $"Exception: [Message:{ex.Message}\nInnerException:{ex.InnerException}\nStackTrace:{ex.StackTrace}]",
                    LogType.Error);
                throw;
            }
        }

        public async Task<Product> Update(Product product)
        {
            try
            {
                var updateResult = _context.Products.Update(product);
                if (await _context.Commit() > 0)
                    return updateResult.Entity;
                return default;
            }
            catch (Exception ex)
            {
                _log.Record(nameof(Update),
                    $"Exception: [Message:{ex.Message}\nInnerException:{ex.InnerException}\nStackTrace:{ex.StackTrace}]",
                    LogType.Error);
                throw;
            }
        }
    }
}
