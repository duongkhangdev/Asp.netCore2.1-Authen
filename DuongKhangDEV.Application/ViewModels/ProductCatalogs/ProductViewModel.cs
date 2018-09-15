using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DuongKhangDEV.Data.Enums;

namespace DuongKhangDEV.Application.ViewModels.ProductCatalog
{
    public class ProductViewModel
    {
        [Required]
        public long Id { get; set; }

        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [StringLength(255)]
        public string Name2 { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int ManufacturerId { get; set; }

        [Required]
        public int VendorId { get; set; }

        [StringLength(255)]
        public string ThumbnailImage { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal Price { get; set; }

        public decimal? PromotionPrice { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal OriginalPrice { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public string Content { get; set; }

        public bool? HomeFlag { get; set; }

        public bool? HotFlag { get; set; }

        public bool? HotSalesFlag { get; set; }

        public bool? HotNewFlag { get; set; }

        public bool? AlwaysOnTheHomePage { get; set; } // Luôn luôn hiển thị trên trang chủ

        public bool? AlwaysOnTheMainMenu { get; set; }  // Luôn luôn hiển thị trên trang chủ - MainMenu

        public bool? AllowAddedToCart { get; set; }  // Cho phép thêm vào giỏ hàng

        public int? ViewCount { get; set; }

        [StringLength(255)]
        public string Tags { get; set; }

        [StringLength(255)]
        public string Unit { get; set; }

        [StringLength(255)]
        public string MetaTitle { set; get; }

        [StringLength(255)]
        public string MetaAlias { set; get; }

        [StringLength(255)]
        public string MetaKeywords { set; get; }

        [StringLength(255)]
        public string MetaDescription { set; get; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { set; get; }

        public Status Status { set; get; }

        public ProductCategoryViewModel ProductCategory { set; get; }
    }
}
