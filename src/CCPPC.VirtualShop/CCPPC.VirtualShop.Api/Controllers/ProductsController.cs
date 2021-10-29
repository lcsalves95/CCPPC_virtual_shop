using CCPPC.VirtualShop.Application.Interfaces;
using CCPPC.VirtualShop.Application.Models;
using CCPPC.VirtualShop.Domain.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CCPPC.VirtualShop.Api.Controllers
{
    [EnableCors("CORSPOLICY")]
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
            RequestResult response = await _service.Insert(model);
            return FormatResponse(response);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> Get(long productId)
        {
            RequestResult response = await _service.Get(productId);
            return FormatResponse(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            RequestResult response = await _service.GetAll();
            return FormatResponse(response);
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> Update(long productId, [FromBody] ProductViewModel model)
        {
            RequestResult response = await _service.Update(productId, model);
            return FormatResponse(response);
        }

        private IActionResult FormatResponse(RequestResult response)
        {
            return response.Status switch
            {
                Domain.Enums.RequestStatus.Error => BadRequest(response),
                Domain.Enums.RequestStatus.NotFound => NotFound(response),
                _ => Ok(response),
            };
        }
    }
}