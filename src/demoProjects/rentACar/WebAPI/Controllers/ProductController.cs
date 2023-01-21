using Application.Features.Models.Models;
using Application.Features.Models.Queries.GetListModelByDynamic;
using Application.Features.ProductDiscount.Dtos;
using Application.Features.Products.Command.CreateProduct;
using Application.Features.Products.Command.UpdateProduct;
using Application.Features.Products.Dtos;
using Application.Features.Products.Models;
using Application.Features.Products.Queries.GetListProduct;
using Application.Services.Amazon;
using Application.Services.Repositories;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Hosting;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        IAmazonService amazonService;
        IDiscountRepository discountRepository;
        IProductDiscountRepository productDiscountRepository;
        BaseDbContext context;
        public ProductController(IAmazonService amazonService, IDiscountRepository discountRepository, IProductDiscountRepository productDiscountRepository, BaseDbContext context)
        {
            this.amazonService = amazonService;
            this.discountRepository = discountRepository;
            this.productDiscountRepository = productDiscountRepository;
            this.context = context;
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
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommand updateProductCommand)
        {
            UpdatedProductDto result = await Mediator.Send(updateProductCommand);
            return Ok(result);
        }
        [HttpGet("getdisc")]
        public async Task<IActionResult> getDiscs(int ProductID)
        {
            var discs = await discountRepository.GetListAsync();
            var prodDiscs = await productDiscountRepository.GetListAsync(x => x.IDProduct == ProductID);

            var disclist = new List<ProductDiscountViewModel>();
            if (discs != null)
            {
                foreach (var item in discs.Items)
                {

                    ProductDiscountViewModel discItem = new ProductDiscountViewModel();
                    var prodItem = prodDiscs.Items.Where(x => x.discountId == item.Id).FirstOrDefault();
                    discItem.added = prodItem != null;
                    discItem.Discount = item;
                    discItem.ProductDiscountID = prodItem != null ? prodItem.Id : 0;
                    disclist.Add(discItem);
                }
            }
            return Ok(disclist);
        }

        [HttpPost("updateDisc")]
        public async Task<IActionResult> updateProductDisc(ProdcutUpdateViewModel updateViewModel)
        {

            context.ProductDiscounts.RemoveRange(context.ProductDiscounts.Where(x => x.IDProduct == updateViewModel.ProductID));

            var product = context.Products.Where(x => x.Id == updateViewModel.ProductID).FirstOrDefault();
            //hangi fiyat değiecek
            if (product == null) return Ok(false);
            product.ExpectedTotalPrice = product.RealPrice;

            await context.SaveChangesAsync();
            context.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
            foreach (var item in updateViewModel.Discs)
            {
                if (item.added)
                {
                    var discItem = new ProductDiscount();

                    discItem.IDProduct = updateViewModel.ProductID;
                    var disc = context.Discounts.Where(x => x.Id == item.DiscountID).AsNoTracking().FirstOrDefault();
                    discItem.discount = disc;



                    discItem.Amount = disc.Disc;
                    if (disc.Multiplier)
                    {

                        product.ExpectedTotalPrice = product.RealPrice * disc.Disc;
                    }
                    else
                    {
                        product.ExpectedTotalPrice = (product.ExpectedStockAmount + disc.Disc) + product.RealPrice;

                    }
                    context.Entry(discItem.discount).State = EntityState.Unchanged;
                    context.Add(discItem);
                }
            }
            context.Update(product);
            await context.SaveChangesAsync();

            return Ok(updateViewModel);
        }


    }

    public class ProdcutUpdateViewModel
    {
        public int ProductID { get; set; }
        public List<ProductDiscountViewModel> Discs { get; set; }
    }
}
