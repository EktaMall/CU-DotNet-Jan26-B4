using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    private readonly IConfiguration _config;

    public HomeController(IConfiguration config)
    {
        _config = config;
    }

    public IActionResult Index()
    {
        ViewBag.PortalName = _config["AppSettings:PortalName"];
        ViewBag.AdminEmail = _config["AppSettings:AdminEmail"];
        return View();
    }
}
