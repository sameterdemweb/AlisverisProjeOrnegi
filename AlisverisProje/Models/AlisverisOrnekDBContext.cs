using AlisverisProje.Entities;
using Microsoft.EntityFrameworkCore;
using AlisverisProje.Models;

namespace AlisverisProje.Models
{
    public class AlisverisOrnekDBContext : DbContext
    {
        public AlisverisOrnekDBContext(DbContextOptions<AlisverisOrnekDBContext> options ): base( options )
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
    
    }
}
