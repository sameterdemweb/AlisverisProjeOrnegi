using System.ComponentModel.DataAnnotations;

namespace AlisverisProje.Entities
{
    public class Customer
    {
        public Customer() // Bir yapı bloğu oluşturmamız lazım ilk kullanıldığında boşta olsa bir değer tanımlanması lazımki hata almayalım!
        {
            Orders = new List<Order>();
        }
        public int Id { get; set; }
        [Display(Name = "Müşteri Adı")]
        public string Name { get; set; }
        [Display(Name = "Yaş Bilgisi")]
        public int Age { get; set; }
        [Display(Name = "Şehir")]
        public string City { get; set; }
        [Display(Name = "Ülke")]
        public string Country { get; set; }
        public List<Order> Orders { get; set; } //Bir müşterinin birden fazla siparişi olabileceği için tanımlıyoruz.
    }
}
