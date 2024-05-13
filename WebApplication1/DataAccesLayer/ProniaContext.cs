using Microsoft.EntityFrameworkCore;
using Pronia.Models;

namespace Pronia.DataAccesLayer
{
    public class ProniaContext : DbContext
    {
        public ProniaContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Slider> Sliders { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }


        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=DESKTOP-8QLA9CR\SQLEXPRESS;Database=Pronia;Trusted_Connection=True;TrustServerCertificate=True");
            base.OnConfiguring(options);
        }
    }
}
