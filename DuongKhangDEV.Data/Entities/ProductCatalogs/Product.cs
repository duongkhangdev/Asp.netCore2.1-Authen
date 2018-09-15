using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using DuongKhangDEV.Data.Enums;
using DuongKhangDEV.Data.Interfaces;
using DuongKhangDEV.Infrastructure.SharedKernel;

namespace DuongKhangDEV.Data.Entities.ProductCatalog
{
    [Table("Products")]
    public class Product : DomainEntity<long>, ISwitchable, IDateTracking, IHasSeoMetaData
    {
        #region Constructors

        public Product()
        {
            ProductTags = new List<ProductTag>();
        }

        public Product(long id, string name, int categoryId, string thumbnailImage,
             decimal price, decimal originalPrice, decimal? promotionPrice,
             string description, string content, bool? homeFlag, bool? hotFlag, bool? hotSalesFlag, bool? hotNewFlag, bool? alwaysOnTheHomePage, bool? alwaysOnTheMainMenu, bool? allowAddedToCart,
             string tags, string unit, Status status, 
             string metaTitle, string metaAlias, string metaKeyword, string metaDescription)
        {
            Id = id;
            Name = name;
            CategoryId = categoryId;
            ThumbnailImage = thumbnailImage;
            Price = price;
            OriginalPrice = originalPrice;
            PromotionPrice = promotionPrice;
            Description = description;
            Content = content;
            HomeFlag = homeFlag;
            HotFlag = hotFlag;
            HotSalesFlag = hotSalesFlag;
            HotNewFlag = hotNewFlag;
            AlwaysOnTheHomePage = alwaysOnTheHomePage;
            AlwaysOnTheMainMenu = alwaysOnTheMainMenu;
            AllowAddedToCart = allowAddedToCart;
            Tags = tags;
            Unit = unit;
            Status = status;
            MetaTitle = metaTitle;
            MetaAlias = metaAlias;
            MetaKeywords = metaKeyword;
            MetaDescription = metaDescription;

            ProductTags = new List<ProductTag>();
        }

        #endregion

        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [Required]
        public int CategoryId { get; set; }

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

        public bool? AlwaysOnTheHomePage { get; set; }  // Luôn luôn hiển thị trên trang chủ

        public bool? AlwaysOnTheMainMenu { get; set; }  // Luôn luôn hiển thị trên trang chủ - MainMenu

        public bool? AllowAddedToCart { get; set; }  // Cho phép thêm vào giỏ hàng

        public int? ViewCount { get; set; }        

        [StringLength(255)]
        public string Tags { get; set; }

        [StringLength(255)]
        public string Unit { get; set; }

        [StringLength(255)]
        public string MetaTitle {set;get;}
        
        [StringLength(255)]
        public string MetaAlias {set;get;}

        [StringLength(255)]
        public string MetaKeywords {set;get;}

        [StringLength(255)]
        public string MetaDescription {set;get;}

        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { set; get; }

        public Status Status {set;get;}

        [ForeignKey("CategoryId")]
        public virtual ProductCategory ProductCategory { set; get; }

        public virtual ICollection<ProductTag> ProductTags { set; get; }
    }
}
