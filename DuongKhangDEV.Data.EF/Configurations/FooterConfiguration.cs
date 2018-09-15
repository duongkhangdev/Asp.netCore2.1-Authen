using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using DuongKhangDEV.Data.EF.Extensions;
using DuongKhangDEV.Data.Entities.Content;

namespace DuongKhangDEV.Data.EF.Configurations
{
    public class FooterConfiguration : DbEntityConfiguration<Footer>
    {
        public override void Configure(EntityTypeBuilder<Footer> entity)
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).IsRequired()
            .IsUnicode(false)
            .HasMaxLength(100);
            // etc.
        }
    }
}
