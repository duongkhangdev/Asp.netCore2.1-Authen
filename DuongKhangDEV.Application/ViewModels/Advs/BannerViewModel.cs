using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DuongKhangDEV.Application.ViewModels.Advs
{
    public class BannerViewModel
    {
        [Required]
        public int Id { get; set; }

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
    }
}
