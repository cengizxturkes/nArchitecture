using Application.Features.OrderItems.Commands.CreateOrderItem;
using Application.Features.OrderItems.Models;
using Application.Features.OrderItems.Queries.GetListOrderItem;
using Application.Features.Products.Command.CreateProduct;
using Application.Features.Products.Models;
using Application.Features.Products.Queries.GetListProduct;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{[Route("api/[controller]")]
        [ApiController]
    public class OrderItemsController : BaseController

    {
        
        [HttpPost("add")]
        public async Task<IActionResult> AddAsync([FromBody] CreateOrderItemCommand createOrderItemCommand)
        {
            var result = await Mediator.Send(createOrderItemCommand);
            return Ok(result);

        }

        [HttpPost("GetList/ByDynamic")]
        public async Task<ActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic)
        {
            GetListOrderItemQuery getListByDynamicOrderItemQuery = new GetListOrderItemQuery { PageRequest = pageRequest, Dynamic = dynamic };
            OrderItemListModel result = await Mediator.Send(getListByDynamicOrderItemQuery);
            return Ok(result);
        }
    }
}
