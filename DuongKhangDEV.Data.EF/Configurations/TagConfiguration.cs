using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DuongKhangDEV.Data.EF.Extensions;
using DuongKhangDEV.Data.Entities;
using Microsoft.EntityFrameworkCore;
using DuongKhangDEV.Data.Entities.Commons;

namespace DuongKhangDEV.Data.EF.Configurations
{
    public class TagConfiguration : DbEntityConfiguration<Tag>
    {
        public override void Configure(EntityTypeBuilder<Tag> entity)
        {
            entity.Property(c => c.Id)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(100);
        }
    }
}
