using Microsoft.EntityFrameworkCore;
using UdemyNLayerProject.Core.Models;
using UdemyNLayerProject.Data.Configurations;
using UdemyNLayerProject.Data.Seeds;

namespace UdemyNLayerProject.Data
{
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Sql Server'da db ye karsılık gelir, tablolara karsılık dbSet'ler bulunur
        /// DB'de tablolar oluşmadan önce çalışacak methodlar burada çağırılır.
        /// </summary>        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }   //isimlendirmenin çoğul olması BP.
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*  modelBuilder.Entity<Product>().Property(x => x.Id).UseIdentityColumn();
                modelBuilder.Entity<Category>().Property(x => x.Id).UseIdentityColumn(); */

            //önce DB'de tablolarım olusacak
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());

            //daha sonra default datalar ilgili tablolara eklenecek
            modelBuilder.ApplyConfiguration(new CategorySeed(new int[] { 1, 2 }));
            modelBuilder.ApplyConfiguration(new ProductSeed(new int[] { 1, 2 }));
        }
    }
}
