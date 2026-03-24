namespace MVC_DI.Services
{
    public class PricingService : IPricingService
    {
        public decimal CalculateFinalPrice(decimal basePrice, string promoCode)
        {
            decimal finalPrice = basePrice;

            if (!string.IsNullOrWhiteSpace(promoCode))
            {
                promoCode = promoCode.Trim().ToUpper();

                if (promoCode == "WINTER25")
                {
                    finalPrice = basePrice - (basePrice * 0.15m);
                }
                else if (promoCode == "FREESHIP")
                {
                    finalPrice = basePrice - 5.00m;
                }
            }

            if (finalPrice < 0)
            {
                finalPrice = 0;
            }

            return finalPrice;
        }
    }
}
