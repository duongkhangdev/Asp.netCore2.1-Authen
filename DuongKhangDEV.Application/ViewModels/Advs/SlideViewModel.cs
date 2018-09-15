using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DuongKhangDEV.Application.ViewModels.Advs
{
    public class SlideViewModel
    {
        [Required]
        public int Id { get; set; }

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
    }
}
