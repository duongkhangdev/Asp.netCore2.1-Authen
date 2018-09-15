using DuongKhangDEV.Data.Interfaces;
using DuongKhangDEV.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DuongKhangDEV.Data.Entities.ProductCatalog
{
    [Table("ProductWishlists")]
    public class ProductWishlist : DomainEntity<int>, IDateTracking
    {
        public ProductWishlist()
        {

        }

        public ProductWishlist(int id, Guid userId, long productId)
        {
            Id = id;
            UserId = userId;
            ProductId = productId;
        }

        public long ProductId { get; set; }

        public Guid UserId { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { set; get; }

        [ForeignKey("ProductId")]
        public virtual Product Product { set; get; }
    }
}
