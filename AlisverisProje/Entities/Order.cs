using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlisverisProje.Entities
{
    public class Order
    {
        public int Id { get; set; }
        [Display(Name = "Müşteri")]
        public int CustomerId { get; set; }
        [Display(Name = "Ürün")]
        public int ProductId { get; set; }
        [Display(Name = "Sipariş Tarihi")]
        public  DateTime OrderDate { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; } //Bir siparişin bir müşterisi olabilir mantığıyla burda müşteriyi tanımladık.
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; } //Bir siparişin bir müşterisi olabilir mantığıyla burda müşteriyi tanımladık.

    }
}
