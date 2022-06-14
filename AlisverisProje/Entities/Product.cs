namespace AlisverisProje.Entities
{
    public class Product
    {
        public Product() // Bir yapı bloğu oluşturmamız lazım ilk kullanıldığında boşta olsa bir değer tanımlanması lazımki hata almayalım!
        {
            Orders = new List<Order>();
        }
        public int Id { get; set; }

        public Category Category { get; set; }//Bir ürünün bir kategorisi olabilir mantığıyla burda müşteriyi tanımladık.

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public decimal Stock { get; set; }

        public List<Order> Orders { get; set; } //Bir Ürün birden fazla siparişi olabileceği için tanımlıyoruz.


    }
}
