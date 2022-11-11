using Application.Features.Models.Models;
using Application.Features.Models.Queries.GetListModelByDynamic;
using Application.Features.Products.Command.CreateProduct;
using Application.Features.Products.Models;
using Application.Features.Products.Queries.GetListProduct;
using Application.Services.Amazon;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        IAmazonService amazonService;

        public ProductController(IAmazonService amazonService)
        {
            this.amazonService = amazonService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddAsync([FromBody] CreateProductCommand createProductCommand)
        {
            var result = await Mediator.Send(createProductCommand);
            return Ok(result);

        }

        [HttpPost("GetList/ByDynamic")]
        public async Task<ActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic)
        {
            GetListProductQuery getListByDynamicModelQuery = new GetListProductQuery { PageRequest = pageRequest, Dynamic = dynamic };
            ProductListModel result = await Mediator.Send(getListByDynamicModelQuery);
            return Ok(result);
        }

        [HttpGet("findFromAmazon")]
        public async Task<IActionResult> FindAmazonProduct([FromQuery] string asincode)
        {

            return Ok(await amazonService.FindAsync(asincode));
        }
    }
}
