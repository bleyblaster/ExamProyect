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
   public class UserMap: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id).HasName("Pk_userid");
            builder.Property(x => x.Id).ValueGeneratedOnAdd().HasColumnName("id").HasColumnType("INT");
            builder.Property(x => x.UserName).HasColumnName("UserName").HasColumnType("VARCHAR(25)");
            builder.Property(x => x.Password).HasColumnName("Password").HasColumnType("VARCHAR(100)");
            builder.Property(x => x.Email).HasColumnName("Email").HasColumnType("VARCHAR(MAX)");
            builder.Property(x => x.IsActive).HasColumnName("IsActive").HasColumnType("bit");
            builder.Property(x => x.ModifiedDate).HasColumnName("ModifiedDate").HasColumnType("DATETIME");

        }

    }
}
