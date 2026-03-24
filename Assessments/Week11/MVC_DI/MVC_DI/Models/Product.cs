namespace MVC_DI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string PromoCode { get; set; }
        public decimal DiscountedPrice { get; set; }
    }
}
