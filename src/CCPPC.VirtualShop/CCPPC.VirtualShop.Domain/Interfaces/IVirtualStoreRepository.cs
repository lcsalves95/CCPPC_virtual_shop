using CCPPC.VirtualShop.Domain.Entities;
using System.Threading.Tasks;

namespace CCPPC.VirtualShop.Domain.Interfaces
{
    public interface IVirtualStoreRepository
    {
        Task<Order> GetOpenOrder(long userId);
        Task<bool> AddItemOrder(ItemOrder itemOrder);
        Task<Order> AddOrder(Order order);
    }
}
