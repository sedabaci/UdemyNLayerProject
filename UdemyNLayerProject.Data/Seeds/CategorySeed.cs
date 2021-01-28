using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UdemyNLayerProject.Core.Models;

namespace UdemyNLayerProject.Data.Seeds
{
    class CategorySeed : IEntityTypeConfiguration<Category>
    {
        /// <summary>
        /// DB'de tablolar olusturulurken, tablolara default data tanımlanması islemi / </summary>
        /// 
        private readonly int[] _ids;
        public CategorySeed(int[] ids)
        {
            _ids = ids;
        }

        public void Configure(EntityTypeBuilder<Category> builder)
        {
            //EF tarafında, sql server tarafı için tabloya default data veriyorsak mutlaka id vermemiz gerekir.
            builder.HasData(
                new Category{Id = _ids[0], Name = "Kalemler"}, 
                new Category{Id = _ids[1], Name = "Defterler"}
            );

        }
    }
}
