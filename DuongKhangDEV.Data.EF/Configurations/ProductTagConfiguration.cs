using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using DuongKhangDEV.Data.EF.Extensions;
using DuongKhangDEV.Data.Entities;
using DuongKhangDEV.Data.Entities.ProductCatalog;

namespace DuongKhangDEV.Data.EF.Configurations
{
    public class ProductTagConfiguration : DbEntityConfiguration<ProductTag>
    {
        public override void Configure(EntityTypeBuilder<ProductTag> entity)
        {
            entity.Property(c => c.TagId).IsRequired()
            .IsUnicode(false)
            .HasMaxLength(100);
            // etc.
        }
    }
}
