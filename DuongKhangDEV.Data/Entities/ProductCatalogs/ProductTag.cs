using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using DuongKhangDEV.Data.Entities.Commons;
using DuongKhangDEV.Infrastructure.SharedKernel;

namespace DuongKhangDEV.Data.Entities.ProductCatalog
{
    [Table("ProductTags")]
    public class ProductTag : DomainEntity<int>
    {
        public long ProductId { get; set; }
        
        public string TagId { set; get; }

        [ForeignKey("ProductId")]
        public virtual Product Product { set; get; }

        [ForeignKey("TagId")]
        public virtual Tag Tag { set; get; }
    }
}
