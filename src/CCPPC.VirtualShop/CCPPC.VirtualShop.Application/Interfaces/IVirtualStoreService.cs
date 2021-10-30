using CCPPC.VirtualShop.Application.Models;
using CCPPC.VirtualShop.Domain.Models;
using System.Threading.Tasks;

namespace CCPPC.VirtualShop.Application.Interfaces
{
    public interface IVirtualStoreService
    {
        Task<RequestResult> AddItemOrder(long userId, ItemOrderViewModel model);
        Task<RequestResult> GetOpenOrder(long userId);
    }
}
