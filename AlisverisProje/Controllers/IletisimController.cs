using AlisverisProje.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlisverisProje.Controllers
{
    public class IletisimController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(IletisimForm iletisimForm )
        {


            return View();
        }

    }
}
