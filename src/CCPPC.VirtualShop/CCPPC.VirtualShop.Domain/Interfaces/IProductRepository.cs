using CCPPC.VirtualShop.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CCPPC.VirtualShop.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> Insert(Product product);
        Task<Product> Update(Product product);
        Task<Product> Get(long productId);
        Task<IEnumerable<Product>> GetAdd();
    }
}
