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

        [Required(ErrorMessage = "Lütfen kategori seçinimi boş bırakmayın.")]
        public int CategoryId { get; set; }
        [Display(Name = "Ürün Adı")]
        [Required(ErrorMessage = "Lütfen Ürün adını boş bırakmayınız.")]
        public string ProductName { get; set; }

        [Display(Name = "Fiyat")]
        [Required(ErrorMessage = "Lütfen fiyat bilgisini boş bırakmayın.")]
        public decimal Price { get; set; }
        [Display(Name = "Stok Bilgisi")]
        [Required(ErrorMessage = "Lütfen stok bilgisini boş bırakmayın.")]

        public decimal Stock { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }//Bir ürünün bir kategorisi olabilir mantığıyla burda müşteriyi tanımladık.
        public virtual List<Order>? Orders { get; set; } //Bir Ürün birden fazla siparişi olabileceği için tanımlıyoruz.


    }
}
