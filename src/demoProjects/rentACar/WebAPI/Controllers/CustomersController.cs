using Application.Features.Customers.Commands.CreateCustomer;
using Application.Features.Customers.Models;
using Application.Features.Customers.Queries.GetListCustomer;
using Application.Features.Products.Command.CreateProduct;
using Application.Features.Products.Models;
using Application.Features.Products.Queries.GetListProduct;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class CustomersController : BaseController
    {

        [HttpPost("add")]
        public async Task<IActionResult> AddAsync([FromBody] CreatedCustomerCommand createdCustomerCommand)
        {
            var result = await Mediator.Send(createdCustomerCommand);
            return Ok(result);

        }

        [HttpPost("GetList/ByDynamic")]
        public async Task<ActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic)
        {
            GetListCustomerQuery getListCustomerQuery = new GetListCustomerQuery { PageRequest = pageRequest, Dynamic = dynamic };
            CustomerListModel result = await Mediator.Send(getListCustomerQuery);
            return Ok(result);
        }
    }
}
