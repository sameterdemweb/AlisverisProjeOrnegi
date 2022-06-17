using AlisverisProje.Entities;

using AlisverisProje.Models;

using Microsoft.EntityFrameworkCore;

namespace AlisverisProje.Models
{
    public class AlisverisOrnekDBContext : DbContext
    {

        public AlisverisOrnekDBContext(DbContextOptions<AlisverisOrnekDBContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }


        // Çalışan tablolarımız ve örnek data eklemek!

        public DbSet<Calisanlar> Calisanlar { get; set; }
        public DbSet<Adresler> Adresler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adresler>().HasData(
                    new Calisanlar
                    {
                        Id = 1,
                        Ad = "Samet",
                        Soyad = "Erdem"
                    }
                );
        }
    }
}
 

