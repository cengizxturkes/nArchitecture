using Domain.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Persistence.Contexts;
using System.Security.Claims;
using WebUi.Models.AuthControllerModel;
using WebUi.Models.TokenModels;
using WebUi.ServicesPrice;
using System.Linq;

namespace WebUi.Controllers
{
    public class EditProductController : BaseController
    {
        private readonly BaseDbContext _context;
        public EditProductController(BaseDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> IndexAsync(int id,int fnsku, int boxlabeling)
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
            var cengiz =SelectedProductList.Select(x => x.ExpectedTotalPrice).FirstOrDefault();
            var TotalStock=SelectedProductList.Select(x => x.ExpectedStockAmount).FirstOrDefault();
            var toplam = cengiz / TotalStock * .3;
            
            //diğğerleri de etkilicek demi konuuutumuz gibi
            //yo o deil 3 toggle var a tamamdır
           
            //ürün hhangisi mkk
                var ProductQuantity = _context.Products.Where(x => x.Id == id).Select(y=>y.ExpectedStockAmount).FirstOrDefault();
            var ProductExpectedTotalPrice = _context.Products.Where(x => x.Id == id).Select(y=>y.ExpectedTotalPrice).FirstOrDefault();
            var ExpectedTotalQuantity = _context.Products.Where(x => x.Id == id).Select(y => y.ExpectedStockAmount).FirstOrDefault();
            var Fnsku = ExpectedTotalQuantity * 0.30;
            var ProductDesi = _context.Products.Where(x => x.Id == id).Select(y=>y.Desi).FirstOrDefault();
            var FNSKU = (ProductQuantity * FnskuLabelling);
             FNSKU = Math.Round(FNSKU, 2);
            if (fnsku == 1)
            {

                var response = await _client.PostAsJsonAsync("Product/update", SelectedProductList);
                if (response.IsSuccessStatusCode)
                {

                }
            }


            var boxlabelingupdate = boxlabeling == 0 ? ProductExpectedTotalPrice : 3 + ProductExpectedTotalPrice; 

            //if (fnsku == 0)
            //{
            //    @ViewBag.ExpectedTotalCostWithoutTotal = ProductExpectedTotalPrice;
            //}
            //else
            //{
            //    @ViewBag.ExpectedTotalCostWithoutTotal = FNSKU + ProductExpectedTotalPrice;
            //}

            ViewBag.ProductQuantity = ProductQuantity;
           
            ViewBag.ProductQuantityFNSKU = FNSKU;
            double Boxing = ProductDesi<=50?3:6;
            
            ViewBag.Orders = fundList;
            ViewBag.name = userName;
            ViewBag.v = usermail;
            ViewBag.SelectedProductList = SelectedProductList;
            ViewBag.ProductID = SelectedProductList.First().Id;
            //bu neden liste mk tek ürün ok mu id'yye ait
            //hayır çok ürün olabilir


            return View(fundList);


            
        } 


        [HttpPost]
        public async Task<IActionResult> EditFnsku(int id,int fnsku)
        {
            var ExpectedTotalProductPrice = _context.Products.Where(x => x.Id == id).Select(y => y.ExpectedTotalPrice).FirstOrDefault();
            var ExpectedTotalQuantity = _context.Products.Where(x => x.Id == id).Select(y => y.ExpectedStockAmount).FirstOrDefault();
            var Fnsku = ExpectedTotalQuantity * 0.30;
           

            return RedirectToAction("Index","Inventory", ExpectedTotalProductPrice);
        }
        public async Task<IActionResult> Edited(double ExpectedTotalProductPrice)
        {
            var Exped = ExpectedTotalProductPrice;



            return View("Index",ExpectedTotalProductPrice);
        }

    }
}
