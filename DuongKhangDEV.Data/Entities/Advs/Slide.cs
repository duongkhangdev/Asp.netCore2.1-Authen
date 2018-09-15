using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using DuongKhangDEV.Infrastructure.SharedKernel;

namespace DuongKhangDEV.Data.Entities.Advs
{
    [Table("Slides")]
    public class Slide : DomainEntity<int>
    {
        #region Constructors

        public Slide()
        {

        }

        public Slide(int id, string name, string title, string description, string thumbnailImage, string url, int groupId, int? displayOrder, int? slideOrder, bool status, 
            string content1, string content2, string content3, string content4, string content5, string groupAlias, string target, string buttonText)
        {
            Id = id;
            Name = name;
            Title = title;
            Description = description;
            ThumbnailImage = thumbnailImage;
            Url = url;
            GroupId = groupId;
            DisplayOrder = displayOrder;
            SlideOrder = slideOrder;
            Status = status;
            Content1 = content1;
            Content2 = content2;
            Content3 = content3;
            Content4 = content4;
            Content5 = content5;
            GroupAlias = groupAlias;
            Target = target;
            ButtonText = buttonText;
        }

        #endregion

        [StringLength(255)]
        [Required]
        public string Name { set; get; }

        [StringLength(255)]
        public string Title { set; get; }

        [StringLength(500)]
        public string Description { set; get; }

        [StringLength(255)]
        [Required]
        public string ThumbnailImage { set; get; }

        [StringLength(500)]
        public string Url { set; get; }

        public int? DisplayOrder { set; get; }

        public int? SlideOrder { set; get; } // Thứ tự đặt Slide

        public bool Status { set; get; }

        [StringLength(500)]
        public string Content1 { set; get; }

        [StringLength(500)]
        public string Content2 { set; get; }

        [StringLength(500)]
        public string Content3 { set; get; }

        [StringLength(500)]
        public string Content4 { set; get; }

        [StringLength(500)]
        public string Content5 { set; get; }

        [StringLength(25)]
        [Required]
        public string GroupAlias { get; set; }

        [StringLength(25)]
        [Required]
        public string Target { get; set; }

        [StringLength(255)]
        public string ButtonText { get; set; }

        [Required]
        public int GroupId { set; get; }

        [ForeignKey("GroupId")]
        public virtual SlideGroup SlideGroup { set; get; }
    }
}
