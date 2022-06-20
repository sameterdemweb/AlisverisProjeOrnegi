using AlisverisProje.Entities;
using AlisverisProje.Identity;
using AlisverisProje.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlisverisProje.Controllers
{
    public class CalisanController : Controller
    {

      private readonly AppIdentityDbContext _context;

        public CalisanController(AppIdentityDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            #region DataSeeder ile fake datalar oluşturma.
           /* DataSeeder datam = new DataSeeder(_context);
            datam.Seed();*/
            #endregion
          
            
            List<Calisanlar> calisanlar = _context.Calisanlar.ToList(); // Çalışanlar tipinde bir calisanlar listesi oluşturup ilgili tablodan çektik.

            List<Adresler> adresler = _context.Adresler.ToList(); // Çalışanlar tipinde bir calisanlar listesi oluşturup ilgili tablodan çektik.

            CalisanIndexViewModel calisanViewModel = new CalisanIndexViewModel
            {
                Calisanlar = calisanlar,
                Adresler=adresler
            };

            return View(calisanViewModel);
        }
    }
}
