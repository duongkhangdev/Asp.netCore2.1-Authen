using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DuongKhangDEV.Data.Enums;
using DuongKhangDEV.Data.Interfaces;
using DuongKhangDEV.Infrastructure.SharedKernel;

namespace DuongKhangDEV.Data.Entities.ProductCatalog
{
    [Table("ProductCategories")]
    public class ProductCategory : DomainEntity<int>, IHasSeoMetaData, ISwitchable, ISortable, IDateTracking
    {
        #region Constructors

        public ProductCategory()
        {
            Products = new List<Product>();
        }

        public ProductCategory(int id, string name, string description, int? parentId, int? homeOrder,
            string thumbnailImage, string uniqueCode, bool? homeFlag, int sortOrder, Status status,
            string metaTitle, string metaAlias, string metaKeywords, string metaDescription)
        {
            Id = id;
            Name = name;
            Description = description;
            ParentId = parentId;
            HomeOrder = homeOrder;
            ThumbnailImage = thumbnailImage;
            UniqueCode = uniqueCode;
            HomeFlag = homeFlag;
            SortOrder = sortOrder;
            Status = status;
            MetaTitle = metaTitle;
            MetaAlias = metaAlias;
            MetaKeywords = metaKeywords;
            MetaDescription = metaDescription;
        }
                
        #endregion

        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int? ParentId { get; set; }

        public int? HomeOrder { get; set; }

        [StringLength(255)]
        public string ThumbnailImage { get; set; }

        [StringLength(50)]
        public string UniqueCode { get; set; }

        public bool? HomeFlag { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { set; get; }

        public int SortOrder { set; get; }

        public Status Status { set; get; }

        [StringLength(255)]
        public string MetaTitle { set; get; }

        [StringLength(255)]
        public string MetaAlias { set; get; }

        [StringLength(255)]
        public string MetaKeywords { set; get; }

        [StringLength(255)]
        public string MetaDescription { set; get; }

        public virtual ICollection<Product> Products { set; get; }
    }
}