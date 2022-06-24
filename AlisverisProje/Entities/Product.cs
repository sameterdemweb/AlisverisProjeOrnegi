using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Required]
        public int CategoryId { get; set; }
        [Display(Name = "Ürün Adı")]
        [Required]
        public string ProductName { get; set; }

        [Display(Name = "Fiyat")]
        [Required]
        public decimal Price { get; set; }
        [Display(Name = "Stok Bilgisi")]
        [Required]
        public decimal Stock { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }//Bir ürünün bir kategorisi olabilir mantığıyla burda müşteriyi tanımladık.
        public virtual List<Order> Orders { get; set; } //Bir Ürün birden fazla siparişi olabileceği için tanımlıyoruz.


    }
}
