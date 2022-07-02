using AlisverisProje.Entities;
using AlisverisProje.Identity;
namespace AlisverisProje.Models
{
    public class DataSeeder
    {
        private readonly AppIdentityDbContext _context;

        public DataSeeder(AppIdentityDbContext context)
        {
            this._context = context;
        }

        public void Seed()
        {
            if (_context.Calisanlar.Any())
            {

                //Kişiler içeri aktarılıyor.
                for (int i = 1; i < 11; i++)
                {

                    Calisanlar calisan = new Calisanlar() { 
                        Ad = FakeData.NameData.GetFirstName(), Soyad = FakeData.NameData.GetSurname(), 
                        Yas = FakeData.NumberData.GetNumber(10, 90) };
                    _context.Calisanlar.Add(calisan);
                }
                _context.SaveChanges();

                List<Calisanlar> calisanList = _context.Calisanlar.ToList();

                //Adresler içeri aktarılıyor.
                foreach (var CalisanBilgi in calisanList)
                {
                    for (int i = 0; i < FakeData.NumberData.GetNumber(1,5); i++)
                    {
                        Adresler adres = new Adresler { 
                            AdresBilgisi = FakeData.PlaceData.GetAddress(), 
                            kisi= CalisanBilgi };
                        _context.Adresler.Add(adres);
                    }
                }
                _context.SaveChanges();
            }
        }
    }
}
