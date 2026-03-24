namespace MVC_DI.Services
{

    public interface IPricingService
    {
        decimal CalculateFinalPrice(decimal basePrice, string promoCode);
    }
}
