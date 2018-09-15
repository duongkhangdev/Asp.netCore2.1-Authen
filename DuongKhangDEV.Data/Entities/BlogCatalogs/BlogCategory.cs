using DuongKhangDEV.Data.Enums;
using DuongKhangDEV.Data.Interfaces;
using DuongKhangDEV.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DuongKhangDEV.Data.Entities.BlogCatalog
{
    [Table("BlogCategories")]
    public class BlogCategory : DomainEntity<int>, IHasSeoMetaData, ISwitchable, ISortable, IDateTracking
    {
        #region Constructors

        public BlogCategory()
        {
            BlogInCategories = new List<BlogInCategory>();
        }

        public BlogCategory(int id, string name, string description, int? parentId, int? homeOrder,
            string thumbnailImage, bool? homeFlag, int sortOrder, Status status,
            string metaTitle, string metaAlias, string metaKeywords, string metaDescription)
        {
            Id = id;
            Name = name;
            Description = description;
            ParentId = parentId;
            HomeOrder = homeOrder;
            ThumbnailImage = thumbnailImage;
            HomeFlag = homeFlag;
            SortOrder = sortOrder;
            Status = status;
            MetaTitle = metaTitle;
            MetaAlias = metaAlias;
            MetaKeywords = metaKeywords;
            MetaDescription = metaDescription;
        }

        public BlogCategory(string name, string description, int? parentId, int? homeOrder,
            string thumbnailImage, bool? homeFlag, int sortOrder, Status status,
            string metaTitle, string metaAlias, string metaKeywords, string metaDescription)
        {
            Name = name;
            Description = description;
            ParentId = parentId;
            HomeOrder = homeOrder;
            ThumbnailImage = thumbnailImage;
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

        public bool? HomeFlag { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { set; get; }

        public int SortOrder { set; get; }

        public Status Status { set; get; }

        public string MetaTitle { set; get; }
        public string MetaAlias { set; get; }
        public string MetaKeywords { set; get; }
        public string MetaDescription { set; get; }


        public virtual ICollection<BlogInCategory> BlogInCategories { set; get; }
    }
}
