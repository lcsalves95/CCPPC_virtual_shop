using CCPPC.VirtualShop.Application.Interfaces;
using CCPPC.VirtualShop.Application.Models;
using CCPPC.VirtualShop.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCPPC.VirtualShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VirtualStoreController : ControllerBase
    {
        private readonly IVirtualStoreService _service;

        public VirtualStoreController(IVirtualStoreService service)
        {
            _service = service;
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> AddItemOrder(long userId, [FromBody] ItemOrderViewModel model)
        {
            RequestResult response = await _service.AddItemOrder(userId, model);
            return FormatResponse(response);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetOpenOrder(long userId)
        {
            RequestResult response = await _service.GetOpenOrder(userId);
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
