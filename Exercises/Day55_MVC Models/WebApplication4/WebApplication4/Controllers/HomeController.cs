using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            List<Employee> employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "Ekta Mall", Position = "Manager", Salary = 90000 },
                new Employee { Id = 2, Name = "Kritika Sharma", Position = "SDE-1", Salary = 45000 },
                new Employee { Id = 3, Name = "Kushagar", Position = "HR", Salary = 30000 },
                new Employee { Id = 4, Name = "Tushar Sharma", Position = "Team Lead", Salary = 50000 }
            };

            ViewBag.Announcement = "Please attend the meeting at 3:00 PM.";
            ViewData["Department"] = "IT Department";
            ViewData["IsActive"] = true;

            return View(employees);
        }
    }
}