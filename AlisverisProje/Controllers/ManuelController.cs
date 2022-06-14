using AlisverisProje.Entities;
using AlisverisProje.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlisverisProje.Controllers
{
    public class ManuelController : Controller
    {
        private readonly AlisverisOrnekDBContext _context;

        public ManuelController(AlisverisOrnekDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
          

            var sonucJoin = (from p in _context.Products
                             join c in _context.Categories
                             on p.CategoryId equals c.Id
                             select new
                             {
                                 c.CategoryName,
                                 p.ProductName,
                                 p.Price,
                                 p.Stock
                             } ).ToList(); //Her bir product için "p" ifadesini kullan, Her bir categories için "c"

            /*  if (sonucJoin != null)
              {
                  return View(sonucJoin);
              }
              else
              {
                  return View();
              }*/

            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Product product, int CategoryId)
        {
            if (String.IsNullOrEmpty(CategoryId.ToString()) || CategoryId == 0)
            {
                return Redirect("/Categories/Index");
            }

            Category category = _context.Categories.Find(CategoryId); //ID'sine göre kategoriyi bulma
            category.Product.Add(product); //İlgili kategoriye ürün olarak ekleme
            _context.SaveChanges();  //Değişiklikleri kaydet

            return View();
        }



        public IActionResult MusteriEkleme()
        {
            #region Ekleme İşlemi
            Customer customer1 = new Customer { Name = "Müşteri Adı", City = "Tekirdağ", Country = "Türkiye" }; //Ekleyeceğimiz bir nesneyi oluşturma
            _context.Add(customer1);  // Veritabanına Değer Ekleme İşlemi
            _context.SaveChanges();  //Değişiklikleri kaydet
            #endregion

            return View();
        }


        public IActionResult MusteriSiparisEkleme()
        {
            #region Sipariş Ekleme 
            Customer musteri1 = _context.Customers.Find(1); //ID'sine göre müşteri bilgisi bulundu.
            musteri1.Orders.Add(new Order { OrderDate = DateTime.Now }); //İlgili müşteriye spiariş bilgileri eklendi.
            _context.SaveChanges();  //Değişiklikleri kaydet
            #endregion

            return View();
        }

        public IActionResult MusteriSiparisSilme()
        {
            #region Müşteri Sipariş Silme

            Order siparis1 = _context.Orders.Find(1); //ID'sine göre müşteri siparişini buluyoruz.
            _context.Orders.Remove(siparis1);//ID'ye göre bulunan siparişi siler.
            _context.SaveChanges();  //Değişiklikleri kaydet

            #endregion

            return View();
        }

        public IActionResult MusteriBilgiGuncelleme()
        {
            #region Müşteri Bilgi Güncelleme 

            Customer musteri = _context.Customers.Find(1); //ID'sine göre müşteri bilgisi bulundu.
            musteri.City = "İstanbul";
            _context.SaveChanges();  //Değişiklikleri kaydet

            #endregion

            return View();
        }

        #region Sorgulama Örnekleri 
        public IActionResult SorguGiris()
        {


            IQueryable<Customer> sonucSorgu = from c in _context.Customers
                                              select c; //Her bir müşteri için "c" ifadesini kullan.  Sorgu tipinde değer döndürür.

            List<Customer> sonucListe = (from c in _context.Customers
                                         select c).ToList(); //Her bir müşteri için "c" ifadesini kullan. 

            var sonuc1 = from c in _context.Customers
                         select new { c.Name, c.City, c.Country }; //Her bir müşteri için "c" ifadesini kullan. ve sadece 2 field  çektik


            List<Customer> sonucListe3 = (from c in _context.Customers
                                          where c.City == "Tekirdağ" && c.Country == "Türkiye"
                                          select c).ToList(); //Her bir müşteri için "c" ifadesini kullan. 

            var sonuc2 = (from c in _context.Customers
                          group c by c.City
                                          into g
                          select g).ToList(); //Müşterileri ülke bünyesine göre grupladık ve her bir grubu g isimli tipe atatık ve çektik. Her ülkeyi tek tek gruplar ve tek tek getirir.


            #region OrderBY

            List<Customer> sonucListe4 = (from c in _context.Customers
                                          orderby c.Name
                                          select c).ToList(); //Müşterilerin ileitşim adına göre alfabetik olarak sıralanır.

            List<Customer> sonucListe5 = (from c in _context.Customers
                                          orderby c.Name, c.Country
                                          select c).ToList(); //Müşterilerin önce ülkeye göre sonrada alfabetik olarak isme göre sıralar.

            List<Customer> sonucListe6 = (from c in _context.Customers
                                          orderby c.Name descending
                                          select c).ToList(); //Müşterilerin Adını  alfabetik olarak tersten  sıralar. z-a

            List<Customer> sonucListe7 = (from c in _context.Customers
                                          orderby c.Name ascending
                                          select c).ToList(); //Müşterilerin Adını  alfabetik olarak tersten  sıralar.a-z


            #endregion

            #region Join İşlemi



            var sonucJoin = (from c in _context.Customers
                             join o in _context.Orders
                             on c.Id equals o.CustomerId
                             select new
                             {
                                 c.Id,
                                 c.Name,
                                 o.OrderDate
                             }).ToList(); //Her bir müşteri için "c" ifadesini kullan. 

            #endregion


            var SayfayaDondur = new SorguGirisModelView
            {
                sonuc = sonucSorgu,
                sonucListe = sonucListe,
            };




            return View(SayfayaDondur);
        }
        #endregion
    }
}
