using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UdemyNLayerProject.Core.Models;

namespace UdemyNLayerProject.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        /// <summary>
        /// tablonun çalışma şekli
        /// </summary>
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);  //PK.
            builder.Property(x => x.Id).UseIdentityColumn(); //birer birer artsın
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Stock).IsRequired();
            builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(x => x.InnerBarkod).HasMaxLength(50);
            builder.ToTable("Products");
        }
    }
}
