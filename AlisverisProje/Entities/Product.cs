using System.ComponentModel.DataAnnotations;

namespace AlisverisProje.Entities
{
    public class Product
    {
        public Product() // Bir yapı bloğu oluşturmamız lazım ilk kullanıldığında boşta olsa bir değer tanımlanması lazımki hata almayalım!
        {
            Orders = new List<Order>();
        }
        public int Id { get; set; }
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }
        [Display(Name = "Ürün Adı")]
        public string ProductName { get; set; }

        [Display(Name = "Fiyat")]
        public decimal Price { get; set; }
        [Display(Name = "Stok Bilgisi")]
        public decimal Stock { get; set; }
        public Category Category { get; set; }//Bir ürünün bir kategorisi olabilir mantığıyla burda müşteriyi tanımladık.
        public List<Order> Orders { get; set; } //Bir Ürün birden fazla siparişi olabileceği için tanımlıyoruz.


    }
}
