using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EntityMapper
{
    public class ProductMap: IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id).HasName("Pk_productid");
            builder.Property(x => x.Id).ValueGeneratedOnAdd().HasColumnName("id").HasColumnType("INT");
            builder.Property(x => x.IsActive).HasColumnName("IsActive").HasColumnType("bit");
            builder.Property(x => x.ModifiedDate).HasColumnName("ModifiedDate").HasColumnType("DATETIME");
            builder.Property(x => x.ProductName).HasColumnName("ProductName").HasColumnType("VARCHAR(150)");
            builder.Property(x => x.ProductCode).HasColumnName("ProductCode").HasColumnType("VARCHAR(20)");
            builder.Property(x => x.ExpirationDate).HasColumnName("ExpirationDate").HasColumnType("DATE");
            builder.Property(x => x.CreationDate).HasColumnName("CreationDate").HasColumnType("DATETIME").HasDefaultValue(DateTime.Now);

        }
    }
}
