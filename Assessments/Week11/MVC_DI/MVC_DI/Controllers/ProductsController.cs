using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_DI.Models;
using MVC_DI.Services;

namespace GlobalMart.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IPricingService _pricingService;

        private static List<Product> products = new List<Product>()
        {
            new Product { Id = 1, Name = "Laptop", Price = 50000, PromoCode = "", DiscountedPrice = 50000 },
            new Product { Id = 2, Name = "Headphones", Price = 3000, PromoCode = "", DiscountedPrice = 3000 },
            new Product { Id = 3, Name = "Keyboard", Price = 2000, PromoCode = "", DiscountedPrice = 2000 }
        };

        public ProductsController(IPricingService pricingService)
        {
            _pricingService = pricingService;
        }

        public IActionResult Index()
        {
            foreach (var product in products)
            {
                product.DiscountedPrice = _pricingService.CalculateFinalPrice(product.Price, product.PromoCode);
            }

            return View(products);
        }

        [HttpGet]
        public IActionResult ApplyDiscount(int id)
        {
            var product = products.FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            ApplyDiscountViewModel vm = new ApplyDiscountViewModel();
            vm.Id = product.Id;
            vm.Name = product.Name;
            vm.Price = product.Price;
            vm.PromoCode = product.PromoCode;
            vm.DiscountedPrice = _pricingService.CalculateFinalPrice(product.Price, product.PromoCode);
            vm.PromoCodes = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Select Promo Code", Value = "" },
                new SelectListItem { Text = "WINTER25", Value = "WINTER25" },
                new SelectListItem { Text = "FREESHIP", Value = "FREESHIP" }
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult ApplyDiscount(ApplyDiscountViewModel vm)
        {
            var product = products.FirstOrDefault(x => x.Id == vm.Id);

            if (product == null)
            {
                return NotFound();
            }

            product.PromoCode = vm.PromoCode;
            product.DiscountedPrice = _pricingService.CalculateFinalPrice(product.Price, product.PromoCode);

            return RedirectToAction("Index");
        }
    }
}