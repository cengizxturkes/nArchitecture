using Application.Features.Orders.Commands.CreateOrder;
using Application.Features.Orders.Models;
using Application.Features.Orders.Queries.GetListOrder;
using Application.Features.Products.Command.CreateProduct;
using Application.Features.Products.Models;
using Application.Features.Products.Queries.GetListProduct;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseController
    {
        [HttpPost("add")]
        public async Task<IActionResult> AddAsync([FromBody] CreateOrderCommand createOrderCommand)
        {
            var result = await Mediator.Send(createOrderCommand);
            return Ok(result);

        }

        [HttpPost("GetList/ByDynamic")]
        public async Task<ActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic)
        {
            GetListOrderQuery getListByOrderQuery = new GetListOrderQuery { PageRequest = pageRequest, Dynamic = dynamic };
            OrderListModel result = await Mediator.Send(getListByOrderQuery);
            return Ok(result);
        }
       
    }
}
