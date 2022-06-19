using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlisverisProje.Entities
{
    [Table("Calisanlar")] //Bizim verdiğimiz isim ile kullanımı yapılacak ise.
    public class Calisanlar
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]//Keywords ve Identy (otomatik artan)
        public int Id { get; set; }


        [StringLength(40),Required]// Maksimum 40 karakter ve boş geçilemez olduğunu belirttik
        public string Ad { get; set; }

        [StringLength(40), Required]// Maksimum 40 karakter ve boş geçilemez olduğunu belirttik
        public string Soyad { get; set; }

        public int Yas { get; set; }
        public virtual List<Adresler> Adresler { get; set; }
    }
}
