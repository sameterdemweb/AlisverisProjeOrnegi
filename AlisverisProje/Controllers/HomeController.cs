using AlisverisProje.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AlisverisProje.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["Veri"] = "Bu bir ViewData'da taşınan veridir.";
            ViewBag.Veri = "Bu bir ViewBag'de taşınan veridir.";
            TempData["Veri"] = "Bu bir TempDate'da taşınan veridir.";

            return View();
        }

        public ActionResult Ornek()
        {
            var ViewDataVeri = ViewData["Veri"];
            var ViewBagVeri = ViewBag.Veri;
            var TempDateVeri = TempData["Veri"];

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}