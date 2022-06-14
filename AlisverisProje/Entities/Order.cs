namespace AlisverisProje.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public Customer Customer { get; set; } //Bir siparişin bir müşterisi olabilir mantığıyla burda müşteriyi tanımladık.
        public Product Product { get; set; } //Bir siparişin bir müşterisi olabilir mantığıyla burda müşteriyi tanımladık.

    }
}
