using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MVC_DI.Models
{
    public class ApplyDiscountViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string PromoCode { get; set; }
        public decimal DiscountedPrice { get; set; }
        public List<SelectListItem> PromoCodes { get; set; }
    }
}