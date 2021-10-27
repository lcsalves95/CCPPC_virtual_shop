using CCPPC.VirtualShop.Application.Models;
using CCPPC.VirtualShop.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CCPPC.VirtualShop.Application.Interfaces
{
    public interface IProductService
    {
        Task<Product> Insert(ProductViewModel model);
        Task<Product> Update(long productId, ProductViewModel model);
        Task<Product> Get(long productId);
        Task<IEnumerable<Product>> GetAll();
    }
}
