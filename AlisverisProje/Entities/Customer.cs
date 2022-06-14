namespace AlisverisProje.Entities
{
    public class Customer
    {
        public Customer() // Bir yapı bloğu oluşturmamız lazım ilk kullanıldığında boşta olsa bir değer tanımlanması lazımki hata almayalım!
        {
            Orders = new List<Order>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public List<Order> Orders { get; set; } //Bir müşterinin birden fazla siparişi olabileceği için tanımlıyoruz.
    }
}
