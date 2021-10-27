using CCPPC.VirtualShop.Application.Interfaces;
using CCPPC.VirtualShop.Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CCPPC.VirtualShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] ProductViewModel model)
        {
            return Ok(await _service.AddProduc());
        }
    }
}