using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UdemyNLayerProject.Core.Models;
using UdemyNLayerProject.Data.Configurations;

namespace UdemyNLayerProject.Data
{
    public class AppDbContext : DbContext
    {
        //Sql Server'da db ye karsılık gelir, tablolara karsılık dbSet'ler bulunur
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }   //isimlendirmenin çoğul olması BP.
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*  modelBuilder.Entity<Product>().Property(x => x.Id).UseIdentityColumn();
                modelBuilder.Entity<Category>().Property(x => x.Id).UseIdentityColumn(); */

            //db de tablolar oluşmadan önce çalışacak methodlar burada çağırılır.
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration())
        }
    }
}
