using DuongKhangDEV.Data.Entities.SystemCatalog;
using DuongKhangDEV.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DuongKhangDEV.Application.ViewModels.SystemCatalog
{
    public class MenuViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { set; get; }

        public int? ParentId { set; get; }

        public int? HomeOrder { get; set; }

        [Required]
        [MaxLength(500)]
        public string Url { set; get; }

        [MaxLength(255)]
        public string ThumbnailImage { set; get; }

        [Required]
        [MaxLength(500)]
        public string Description { set; get; }

        [StringLength(50)]
        public string UniqueCode { get; set; }

        public int? DisplayOrder { set; get; }

        [MaxLength(25)]
        public string Target { set; get; }

        public Status Status { set; get; }

        [Required]
        public int GroupId { set; get; }
        
        public virtual MenuGroup MenuGroup { set; get; }
    }
}
