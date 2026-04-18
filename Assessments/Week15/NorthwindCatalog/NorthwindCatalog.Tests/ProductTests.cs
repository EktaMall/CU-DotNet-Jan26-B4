using Xunit;
using NorthwindCatalog.Services.DTOs;

public class ProductTests
{
    [Fact]
    public void InventoryValue_Should_Return_Correct_Value()
    {
        var product = new ProductDto
        {
            UnitPrice = 10,
            UnitsInStock = 5
        };

        var result = product.InventoryValue;

        Assert.Equal(50, result);
    }

    [Fact]
    public void InventoryValue_Should_Be_Zero_When_Stock_Is_Zero()
    {
        var product = new ProductDto
        {
            UnitPrice = 100,
            UnitsInStock = 0
        };

        var result = product.InventoryValue;

        Assert.Equal(0, result);
    }
}