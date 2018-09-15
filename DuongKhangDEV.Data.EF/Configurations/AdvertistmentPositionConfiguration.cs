using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using DuongKhangDEV.Data.EF.Extensions;
using DuongKhangDEV.Data.Entities;
using DuongKhangDEV.Data.Entities.Advs;

namespace DuongKhangDEV.Data.EF.Configurations
{
    public class AdvertistmentPositionConfiguration : DbEntityConfiguration<AdvertistmentPosition>
    {
        public override void Configure(EntityTypeBuilder<AdvertistmentPosition> entity)
        {
            entity.Property(c => c.Id).IsRequired()
            .IsUnicode(false)
            .HasMaxLength(100);
            // etc.
        }
    }
}
