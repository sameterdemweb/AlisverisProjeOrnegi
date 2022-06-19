using AlisverisProje.Models;
using Microsoft.AspNetCore.Mvc;
using AlisverisProje.Services;

using System.Text;
using System.Net.Mail;

namespace AlisverisProje.Controllers
{
    public class IletisimController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Index(IletisimForm iletisimForm)
        {

            string icerik = "İletişim Formundan Mesajınız Var: Formu Gönderen: " + iletisimForm.AdinizSoyadiniz + " Formu Gönderen Telefon Bilgisi: " + iletisimForm.Telefon + " Formu Gönderen Mail Adresi: " + iletisimForm.Mail + " Konu: " + iletisimForm.Konu + " Mesaj: " + iletisimForm.Mesaj + " ";

            #region Kurumsal Mail Atma İşlemi
                MailIslemleri.MailGonderme(icerik,"İletişim Sayfasından Mail Gönderildi");
            #endregion


            return View();
        }

      
    }
}
