using AlisverisProje.Entities;

namespace AlisverisProje
{
    public class SorguGirisModelView
    {
        public IQueryable<Customer> sonuc { get; set; }
        public List<Customer> sonucListe { get; set; }
    }
}