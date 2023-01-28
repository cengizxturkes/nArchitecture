using Application.Features.OrderPayoneer.Command.CreatePayoneerCode;
using Application.Features.Products.Command.CreateProduct;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayoneerController : BaseController
    {

        [HttpPost("add")]
        public async Task<IActionResult> AddAsync([FromBody] CreatePayoneerCommand createPayoneerCommand)
        {
            var result = await Mediator.Send(createPayoneerCommand);
            return Ok(result);

        }
    }
}
