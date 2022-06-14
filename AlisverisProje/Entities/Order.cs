using System.ComponentModel.DataAnnotations;

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
        public DateTime OrderDate { get; set; }
        public Customer Customer { get; set; } //Bir siparişin bir müşterisi olabilir mantığıyla burda müşteriyi tanımladık.
        public Product Product { get; set; } //Bir siparişin bir müşterisi olabilir mantığıyla burda müşteriyi tanımladık.

    }
}
