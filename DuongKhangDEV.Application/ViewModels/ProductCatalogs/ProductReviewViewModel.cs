using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DuongKhangDEV.Application.ViewModels.ProductCatalog
{
    public class ProductReviewViewModel
    {
        public int ReviewId { get; set; }

        public Guid? CustomerId { get; set; }

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
    }
}
