using CCPPC.VirtualShop.Data.Context;
using CCPPC.VirtualShop.Domain.Entities;
using CCPPC.VirtualShop.Domain.Enums;
using CCPPC.VirtualShop.Domain.Interfaces;
using CCPPC.VirtualShop.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCPPC.VirtualShop.Data.Repositories
{
    public class VirtualStoreRepository : IVirtualStoreRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly LogModel _log;

        public VirtualStoreRepository(ApplicationDbContext context)
        {
            _context = context;
            _log = new LogModel();
        }

        public async Task<Order> GetOpenOrder(long userId)
        {
            try
            {
                return await _context.Orders
                    .FirstOrDefaultAsync(x => x.UserId == userId && (x.Status == OrderStatus.Initiate || x.Status == OrderStatus.NotAuthorized));
            }
            catch(Exception ex)
            {
                _log.Record(nameof(GetOpenOrder),
                    $"Exception: [Message:{ex.Message}\nInnerException:{ex.InnerException}\nStackTrace:{ex.StackTrace}]",
                    LogType.Error);
                throw;
            }
        }

        public async Task<bool> AddItemOrder(ItemOrder itemOrder)
        {
            try
            {
                var insertResult = await _context.ItemOrders.AddAsync(itemOrder);
                return await _context.Commit() > 0;
            }
            catch(Exception ex)
            {
                _log.Record(nameof(AddItemOrder),
                    $"Exception: [Message:{ex.Message}\nInnerException:{ex.InnerException}\nStackTrace:{ex.StackTrace}]",
                    LogType.Error);
                throw;
            }
        }

        public async Task<Order> AddOrder(Order order)
        {
            try
            {
                var insertResult = await _context.Orders.AddAsync(order);
                return await _context.Commit() > 0 ? order : default;
            }
            catch (Exception ex)
            {
                _log.Record(nameof(AddOrder),
                    $"Exception: [Message:{ex.Message}\nInnerException:{ex.InnerException}\nStackTrace:{ex.StackTrace}]",
                    LogType.Error);
                throw;
            }
        }
    }
}
