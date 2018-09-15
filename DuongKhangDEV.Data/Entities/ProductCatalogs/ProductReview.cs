using DuongKhangDEV.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DuongKhangDEV.Data.Entities.ProductCatalog
{
    [Table("ProductReviews")]
    public class ProductReview : DomainEntity<int>
    {
        
        public Guid? CustomerId { get; set; }

        [Required]
        public long ProductId { get; set; }
        
        public bool? IsApproved { get; set; }
        
        [MaxLength(255)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string ReviewText { get; set; }

        [MaxLength(500)]
        public string ReplyText { get; set; }
        
        public int? QualityRating { get; set; }
        
        public int? PriceRating { get; set; }
        
        public int? ValueRating { get; set; }
        
        public DateTime? CreatedOn { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
