using CCPPC.VirtualShop.Application.Models;
using CCPPC.VirtualShop.Domain.Models;
using System.Threading.Tasks;

namespace CCPPC.VirtualShop.Application.Interfaces
{
    public interface IProductService
    {
        Task<RequestResult> Insert(ProductViewModel model);
        Task<RequestResult> Update(long productId, ProductViewModel model);
        Task<RequestResult> Get(long productId);
        Task<RequestResult> GetAll();
    }
}
