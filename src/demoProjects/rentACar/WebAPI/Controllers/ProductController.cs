using Application.Features.Models.Models;
using Application.Features.Models.Queries.GetListModelByDynamic;
using Application.Features.OrderPayoneer.Dtos;
using Application.Features.ProductDiscount.Dtos;
using Application.Features.ProductPdf.Dtos;
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
using Persistence.Migrations;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IHostEnvironment webHostEnvironment;
        IProductPdfRepository productPdfRepository;
        IFileHelper formFile;
        IAmazonService amazonService;
        IDiscountRepository discountRepository;
        IProductDiscountRepository productDiscountRepository;
        IProductRepository productRepository;
        BaseDbContext context;
        public ProductController(IAmazonService amazonService, IDiscountRepository discountRepository, IProductDiscountRepository productDiscountRepository, BaseDbContext context, IProductPdfRepository productPdfRepository, IHostEnvironment webHostEnvironment, IFileHelper formFile)
        {
            this.amazonService = amazonService;
            this.discountRepository = discountRepository;
            this.productDiscountRepository = productDiscountRepository;
            this.context = context;
            this.productPdfRepository = productPdfRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.formFile = formFile;
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
            double newprice = 0;
            double constprice = product.RealPrice;
            foreach (var item in updateViewModel.Discs)
            {
                if (item.added)
                {
                    var discItem = new ProductDiscount();

                    discItem.IDProduct = updateViewModel.ProductID;
                    var disc = context.Discounts.Where(x => x.Id == item.DiscountID).AsNoTracking().FirstOrDefault();
                    discItem.discount = disc;


                    
                    discItem.Amount = disc.Disc;
                    if (disc.Multiplier==0)
                    {
                        newprice = (product.ExpectedStockAmount * disc.Disc)+newprice;
                    }
                    //else if
                    //{
                    //    newprice =  (product.ExpectedStockAmount+disc.Disc) +product.RealPrice;
                    //}
                    if (disc.Multiplier == 1)
                    {
                        newprice = (product.Box * disc.Disc) + newprice;

                    }
                    if (disc.Multiplier == 2)
                    {
                        newprice = (product.ExpectedStockAmount/2 * disc.Disc) + newprice;

                    }
                    if (disc.Multiplier == 3)
                    {
                        newprice = (product.Box  * disc.Disc) + newprice;

                    }
                    context.Entry(discItem.discount).State = EntityState.Unchanged;
                    context.Add(discItem);
                    constprice = newprice +product.RealPrice;
                }
            }
            product.ExpectedTotalPrice = constprice;
            context.Update(product);
            await context.SaveChangesAsync();
            return Ok(updateViewModel);
        }
        [HttpPost("AddPdf")]
        public async Task<IActionResult> AddPdfAsync(ProductPdfViewModel productPdfViewModel)
        {

            string UploadPath = Path.Combine(webHostEnvironment.ContentRootPath, "Assets");
            if (!Directory.Exists(UploadPath))
                Directory.CreateDirectory(UploadPath);
            foreach (var item in productPdfViewModel.Files)
            {
                var name = 
                    formFile.SaveFileFromBase64(Request, item.File, item.Name);
                productPdfRepository.Add(new ProductPdf() { FilePath = name, IDProduct = productPdfViewModel.ProductId });

            }
            return Ok();
        }
       
        [HttpPost("DeletePdf")]
        public async Task<IActionResult> DeletePdf(int ProductID)
        {

            var productpdf = context.ProductPdfs.Where(x => x.IDProduct == ProductID).Select(y => y.Id);
            context.Entry(productpdf).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;

            context.Remove(productpdf);

            await context.SaveChangesAsync();

            return Ok(productpdf);
        }


    }

    public class ProdcutUpdateViewModel
    {
        public int ProductID { get; set; }
        public List<ProductDiscountViewModel> Discs { get; set; }
    }
    public class ProductUpdatePdfViewModel
    {
        public int ProductID { get; set; }
        public List<ProductPdfViewModel>ProductPdfs { get; set; }

    }
    public class FileHelper : IFileHelper
    {
        private readonly IHostEnvironment _env;
        public FileHelper(IHostEnvironment env)
        {
            _env = env;
        }
        public string SaveFileFromBase64(HttpRequest Request, string base64, string ext, string path = "")
        {
            string filedir = Path.Combine(_env.ContentRootPath, "Assets", path);
            if (!Directory.Exists(filedir))
            { //check if the folder exists;
                Directory.CreateDirectory(filedir);
            }
            var filepath = @$"{Guid.NewGuid().ToString()}." + ext;
            base64 = base64.Split(',').Last() ?? "";
            base64 = base64.Replace("data:image/png;base64,", "").Replace("data:image/jpeg;base64,", "").Replace("data:image/jpg;base64,", "");
            byte[] bytes = Convert.FromBase64String(base64.Replace("data:image/png;base64,", ""));
            if (bytes.Length > 0)
            {
                using (var stream = new FileStream(Path.Combine(filedir, filepath), FileMode.Create))
                {
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Flush();
                }
            }
            var url = String.Format("{0}://{1}/", Request.Scheme, Request.Host);
            filepath = url + Path.Join("Assets", path, filepath);
            return filepath;
        }
    }
    public interface IFileHelper
    {
        string SaveFileFromBase64(HttpRequest Request, string base64, string ext, string path = "");
    }
}
