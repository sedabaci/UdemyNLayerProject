using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UdemyNLayerProject.Core.Models;

namespace UdemyNLayerProject.Data.Configurations
{
    public class PersonConfiguration: IEntityTypeConfiguration<Person>
    {
        ///tablonun calısma sekli

        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(x=> x.Id);
            builder.Property(x=> x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Surname).IsRequired().HasMaxLength(100);
        }
    }
}
