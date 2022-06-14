using System.ComponentModel.DataAnnotations;

namespace AlisverisProje.Entities
{
    public class Category
    {
        public Category() // Bir yapı bloğu oluşturmamız lazım ilk kullanıldığında boşta olsa bir değer tanımlanması lazımki hata almayalım!
        {
            Product = new List<Product>();
        }

        public int Id { get; set; }

        [Display(Name ="Kategori Adı")]
        public string CategoryName { get; set; }
        public List<Product> Product { get; set; } //Bir kategorinin birden fazla ürüne ait olabileceği için tanımlıyoruz.
    }
}
