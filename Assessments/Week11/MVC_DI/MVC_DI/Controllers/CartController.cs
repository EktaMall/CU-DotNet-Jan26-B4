using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_DI.Models;
using MVC_DI.Services;

namespace GlobalMart.Controllers
{
    public class CartController : Controller
    {
        private readonly IPricingService _pricingService;

        private static List<Product> cartItems = new List<Product>()
        {
            new Product { Id = 1, Name = "Laptop", Price = 50000, PromoCode = "" },
            new Product { Id = 2, Name = "Headphones", Price = 3000, PromoCode = "" }
        };

        public CartController(IPricingService pricingService)
        {
            _pricingService = pricingService;
        }

        public IActionResult Index()
        {
            decimal total = 0;

            foreach (var item in cartItems)
            {
                item.DiscountedPrice = _pricingService.CalculateFinalPrice(item.Price, item.PromoCode);
                total += item.DiscountedPrice;
            }

            ViewBag.GrandTotal = total;

            return View(cartItems);
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            ViewBag.PromoCodes = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Select Promo Code", Value = "" },
                new SelectListItem { Text = "WINTER25", Value = "WINTER25" },
                new SelectListItem { Text = "FREESHIP", Value = "FREESHIP" }
            };

            return View();
        }

        [HttpPost]
        public IActionResult Checkout(string promoCode)
        {
            decimal total = 0;

            foreach (var item in cartItems)
            {
                item.PromoCode = promoCode;
                item.DiscountedPrice = _pricingService.CalculateFinalPrice(item.Price, item.PromoCode);
                total += item.DiscountedPrice;
            }

            ViewBag.FinalTotal = total;
            ViewBag.AppliedCode = promoCode;
            ViewBag.PromoCodes = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Select Promo Code", Value = "" },
                new SelectListItem { Text = "WINTER25", Value = "WINTER25" },
                new SelectListItem { Text = "FREESHIP", Value = "FREESHIP" }
            };

            return View();
        }
    }
}