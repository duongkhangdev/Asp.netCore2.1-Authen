using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using DuongKhangDEV.Infrastructure.SharedKernel;

namespace DuongKhangDEV.Data.Entities.ProductCatalog
{
    [Table("ProductImages")]
    public class ProductImage : DomainEntity<long>
    {
        public long ProductId { get; set; }

        [StringLength(250)]
        public string Path { get; set; }

        [StringLength(250)]
        public string Caption { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
