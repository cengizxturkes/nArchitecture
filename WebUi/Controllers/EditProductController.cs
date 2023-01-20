using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Contexts;
using WebUi.ServicesPrice;

namespace WebUi.Controllers
{
    public class EditProductController : Controller
    {
        private readonly BaseDbContext _context;
        public EditProductController(BaseDbContext context)
        {
            _context = context;
        }
        [Authorize]
        public IActionResult Index(int id,ServicesPrices servicesPrices)
        {

            double GoodAcceptance = 0.15;
            double FnskuLabelling = 0.30;
            double PolyBagging = 0.50;
            double BubbleWrapping = 0.70;
            double ReBoxing = 5;
            double MultiPack = 2.5;
            double ShippingLabeling = 2.5;
            double FbaBoxLabeling = 2.5;
            double PaletInOut = 5;
            double Palletizing = 40;
            double bundle2 = 0.75;
            double bundle35 = 1.20;
            double bundle610 = 1.60;
            double bundle11100 = 1.8;
            double bundle101 = 1;
            
            var usermail = User.Identity.Name;
            var userName = _context.Users.Where(x => x.Email == usermail).Select(y => y.FirstName).FirstOrDefault();
            var userLastName = _context.Users.Where(x => x.Email == usermail).Select(y => y.LastName).FirstOrDefault();
            var userId = _context.Users.Where(x => x.Email == usermail).Select(y => y.Id).FirstOrDefault();
            ViewBag.LastName = userLastName;
            List<Product> fundList = _context.Products.Where(x => x.UserId == userId).ToList();
            List<Product> SelectedProductList = _context.Products.Where(x => x.Id == id).ToList();
            var ProductQuantity = _context.Products.Where(x => x.Id == id).Select(y=>y.ExpectedStockAmount).FirstOrDefault();
            var ProductExpectedTotalPrice = _context.Products.Where(x => x.Id == id).Select(y=>y.ExpectedTotalPrice).FirstOrDefault();
            var ProductDesi = _context.Products.Where(x => x.Id == id).Select(y=>y.Desi).FirstOrDefault();
            ViewBag.ProductQuantity = ProductQuantity;
            var FNSKU = (ProductQuantity * FnskuLabelling);
             FNSKU = Math.Round(FNSKU, 2);
            ViewBag.ProductQuantityFNSKU = FNSKU;
            double Boxing = ProductDesi<=50?3:6;

            var ExpectedTotalCost = (ProductExpectedTotalPrice * ProductQuantity)+FNSKU+Boxing;

            ViewBag.ExpectedTotalCost = ExpectedTotalCost;
            ViewBag.Orders = fundList;
            ViewBag.name = userName;
            ViewBag.v = usermail;
            ViewBag.SelectedProductList = SelectedProductList;
            return View(fundList);
        }
    }
}
