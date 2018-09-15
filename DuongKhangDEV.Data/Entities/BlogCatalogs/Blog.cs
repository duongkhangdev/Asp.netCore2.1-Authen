using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DuongKhangDEV.Data.Enums;
using DuongKhangDEV.Data.Interfaces;
using DuongKhangDEV.Infrastructure.SharedKernel;

namespace DuongKhangDEV.Data.Entities.BlogCatalog
{
    [Table("Blogs")]
    public class Blog : DomainEntity<int>, ISwitchable, IDateTracking, IHasSeoMetaData
    {
        #region Constructors

        public Blog()
        {
            BlogInCategories = new List<BlogInCategory>();
        }

        public Blog(string name,string thumbnailImage,
           string description, string content, bool? homeFlag, bool? hotFlag,
           string tags, Status status, 
           string metaTitle, string metaAlias, string metaKeyword, string metaDescription)
        {
            Name = name;
            ThumbnailImage = thumbnailImage;
            Description = description;
            Content = content;
            HomeFlag = homeFlag;
            HotFlag = hotFlag;
            Tags = tags;
            Status = status;
            MetaTitle = metaTitle;
            MetaAlias = metaAlias;
            MetaKeywords = metaKeyword;
            MetaDescription = metaDescription;

            BlogInCategories = new List<BlogInCategory>();
        }

        public Blog(int id, string name,string thumbnailImage,
             string description, string content, bool? homeFlag, bool? hotFlag,
             string tags, Status status,
             string metaTitle, string metaAlias, string metaKeyword, string metaDescription)
        {
            Id = id;
            Name = name;
            ThumbnailImage = thumbnailImage;
            Description = description;
            Content = content;
            HomeFlag = homeFlag;
            HotFlag = hotFlag;
            Tags = tags;
            Status = status;
            MetaTitle = metaTitle;
            MetaAlias = metaAlias;
            MetaKeywords = metaKeyword;
            MetaDescription = metaDescription;

            BlogInCategories = new List<BlogInCategory>();
        }

        #endregion

        [Required]
        [MaxLength(255)]
        public string Name { set; get; }

        [MaxLength(255)]
        public string ThumbnailImage { set; get; }

        [MaxLength(500)]
        public string Description { set; get; }

        public string Content { set; get; }

        public bool? HomeFlag { set; get; }
        public bool? HotFlag { set; get; }
        public int? ViewCount { set; get; }

        public string Tags { get; set; }
                
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { set; get; }

        public Status Status { set; get; }

        [MaxLength(255)]
        public string MetaTitle { set; get; }

        [MaxLength(255)]
        public string MetaAlias { set; get; }

        [MaxLength(255)]
        public string MetaKeywords { set; get; }

        [MaxLength(255)]
        public string MetaDescription { set; get; }

        public virtual ICollection<BlogTag> BlogTags { set; get; }

        public virtual ICollection<BlogInCategory> BlogInCategories { set; get; }
    }
}