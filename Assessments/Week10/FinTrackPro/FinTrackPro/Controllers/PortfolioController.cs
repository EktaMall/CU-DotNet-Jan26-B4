using FinTrackPro.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinTrackPro.Controllers
{
    public class PortfolioController : Controller
    {
        private static List<Asset> assets = new List<Asset>
        {
            new Asset{Id=1, Name="Tata", Value=5000},
            new Asset{Id=2, Name="Tesla", Value=7000},
            new Asset{Id=3, Name="HDFC Bank", Value=3000}
        };

        public IActionResult Index()
        {
            ViewData["Total"] = assets.Sum(a => a.Value);
            return View(assets);
        }

        [Route("Asset/Info/{id:int}")]
        public IActionResult Details(int id)
        {
            var asset = assets.FirstOrDefault(x => x.Id == id);
            return View(asset);
        }

        public IActionResult Delete(int id)
        {
            var asset = assets.FirstOrDefault(x => x.Id == id);

            if (asset != null)
            {
                assets.Remove(asset);
                TempData["Message"] = "Asset Deleted Successfully";
            }

            return RedirectToAction("Index");
        }
    }
}
