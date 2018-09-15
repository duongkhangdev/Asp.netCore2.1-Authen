using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using DuongKhangDEV.Data.EF.Extensions;
using DuongKhangDEV.Data.Entities;

namespace DuongKhangDEV.Data.EF.Configurations
{
    public class PageConfiguration : DbEntityConfiguration<Page>
    {
        public override void Configure(EntityTypeBuilder<Page> entity)
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).HasMaxLength(255).IsRequired()
                .IsUnicode(false);
            // etc.
        }
    }
}
