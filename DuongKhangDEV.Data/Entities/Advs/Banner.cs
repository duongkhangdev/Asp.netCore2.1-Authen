using DuongKhangDEV.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DuongKhangDEV.Data.Entities.Advs
{
    [Table("Banners")]
    public class Banner : DomainEntity<int>
    {
        #region Constructors

        public Banner()
        {

        }

        public Banner(int id, string name, string title, string positionDescription, string description, string thumbnailImage, string url, int groupId, int? displayOrder, int? bannerOrder, bool status, string content, string groupAlias, string buttonText, string target)
        {
            Id = id;
            Name = name;
            Title = title;
            PositionDescription = positionDescription;
            Description = description;
            ThumbnailImage = thumbnailImage;
            Url = url;
            GroupId = groupId;
            DisplayOrder = displayOrder;
            BannerOrder = bannerOrder;
            Status = status;
            Content = content;
            GroupAlias = groupAlias;
            ButtonText = buttonText;
            Target = target;
        }

        #endregion

        [StringLength(255)]
        [Required]
        public string Name { set; get; }

        [StringLength(255)]
        public string Title { set; get; }

        [StringLength(255)]
        public string PositionDescription { set; get; }

        [StringLength(500)]
        public string Description { set; get; }

        [StringLength(255)]
        [Required]
        public string ThumbnailImage { set; get; }

        [StringLength(255)]
        public string Url { set; get; }

        public int? DisplayOrder { set; get; }

        public int? BannerOrder { set; get; } // Thứ tự vị trí Banner

        public bool Status { set; get; }

        public string Content { set; get; }

        [StringLength(100)]
        [Required]
        public string GroupAlias { get; set; }

        [StringLength(255)]
        public string ButtonText { get; set; }

        [StringLength(25)]
        [Required]
        public string Target { get; set; }

        [Required]
        public int GroupId { set; get; }

        [ForeignKey("GroupId")]
        public virtual BannerGroup BannerGroup { set; get; }
    }
}
