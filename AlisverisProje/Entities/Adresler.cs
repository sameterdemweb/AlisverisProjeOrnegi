using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlisverisProje.Entities
{
    [Table("Adresler")] //Bizim verdiğimiz isim ile kullanımı yapılacak ise.
    public class Adresler
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]//Keywords ve Identy (otomatik artan)
        public int Id { get; set; }
        [StringLength(300)]// Maksimum 300 karakter olduğunu belirttik
        public int AdresBilgisi { get; set; }
        public virtual Calisanlar kisi { get; set; }
    }
}
