using AlisverisProje.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AlisverisProje.Identity
{
    public class AppIdentityDbContext : IdentityDbContext<AppIdentityUser, AppIdentityRole, string>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }



        // EntityFrameworkCore\Add-Migration

        // EntityFrameworkCore\Update-database


        public DbSet<Calisanlar> Calisanlar { get; set; }
        public DbSet<Adresler> Adresler { get; set; }



       protected override void OnModelCreating(ModelBuilder modelBuilder) //Model oluşturulmadan önce override ile ilgili methodu eziyoruz.
        {
            base.OnModelCreating(modelBuilder);
            new DbInitializer(modelBuilder).Seed();//Seed methodunu çağırarak içerisinde oluşturduğumuz örnek dataları oluşturma anında veritabanına ekleme işlemini gerçekleştiriyoruz.
        }

    }
   
    public class DbInitializer
    {
        private readonly ModelBuilder modelBuilder;

        public DbInitializer(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Seed()//Bu method altında istediğimiz tablolara istediğimiz dataları atabiliriz artık.
        {
            for (int i = 1; i < 11; i++)
            {
                modelBuilder.Entity<Calisanlar>().HasData(
                  new Calisanlar { Id = i, Ad = FakeData.NameData.GetFirstName(), Soyad = FakeData.NameData.GetSurname(), Yas = FakeData.NumberData.GetNumber(10, 90) }
                );
            }
        }
    }
}
