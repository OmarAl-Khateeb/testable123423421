using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Config
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            builder.HasOne(p => p.Province).WithMany()
                .HasForeignKey(p => p.ProvinceId);
            builder.HasOne(p => p.UserType).WithMany()
                .HasForeignKey(p => p.UserTypeId);
        }
    }
}