using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using DuongKhangDEV.Infrastructure.SharedKernel;

namespace DuongKhangDEV.Data.Entities.ProductCatalog
{
    [Table("ProductQuantities")]
    public class ProductQuantity : DomainEntity<long>
    {
 
        [Column(Order = 1)]
        public long ProductId { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
