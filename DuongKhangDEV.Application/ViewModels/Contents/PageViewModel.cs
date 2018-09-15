using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DuongKhangDEV.Data.Enums;

namespace DuongKhangDEV.Application.ViewModels.Blog
{
    public class PageViewModel
    {
        [Required]
        public int Id { set; get; }

        [Required]
        [MaxLength(255)]
        public string Name { set; get; }

        [MaxLength(255)]
        [Required]
        public string Alias { set; get; }

        [StringLength(25)]
        [Required]
        public string Target { get; set; }

        public string Content { set; get; }

        public Status Status { set; get; }

        [MaxLength(255)]
        public string MetaTitle { set; get; }

        [MaxLength(255)]
        public string MetaAlias { set; get; }

        [MaxLength(255)]
        public string MetaKeywords { set; get; }

        [MaxLength(255)]
        public string MetaDescription { set; get; }
    }
}
