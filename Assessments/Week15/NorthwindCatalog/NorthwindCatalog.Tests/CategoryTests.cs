using Xunit;
using System.Collections.Generic;
using System.Linq;

public class CategoryTests
{
    [Fact]
    public void AveragePrice_Should_Calculate_Correctly()
    {
        var prices = new List<decimal> { 10, 20, 30 };

        var avg = prices.Average();

        Assert.Equal(20, avg);
    }
}