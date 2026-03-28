using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
public class TrackingController : ControllerBase
{
    [Authorize(Roles = "Admin")]
    [HttpGet("gps")]
    public IActionResult GetGps()
    {
       

        var response = new
        {
            Truck = new
            {
                TruckId = "TRK123",
                Model = "Tata Ultra 1918",
                DriverName = "Ravi Kumar",
                Status = "Moving"
            },
            Location = new
            {
                Latitude = "28.6139",
                Longitude = "77.2090",
                City = "New Delhi"
            },
            Timestamp = DateTime.UtcNow
        };

        return Ok(response);
    }
}