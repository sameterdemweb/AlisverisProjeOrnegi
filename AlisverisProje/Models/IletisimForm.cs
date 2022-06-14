using System.ComponentModel.DataAnnotations;

namespace AlisverisProje.Models
{
    public class IletisimForm
    {
        public int Id { get; set; }

        [Display(Name="Adınız Soyadınız")]
        [Required]
        public string AdinizSoyadiniz { get; set; }

        [Display(Name = "Telefon Numaranız")]
        [Required]
        public string Telefon { get; set; }

        [Display(Name = "Mail Adresi")]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Mail { get; set; }

        [Display(Name = "Konu")]
        [Required]
        public string Konu { get; set; }

        [Display(Name = "Mesajınız")]
        [Required]
        public string Mesaj { get; set; }
    }
}
