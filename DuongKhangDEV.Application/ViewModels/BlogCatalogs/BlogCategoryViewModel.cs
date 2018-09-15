using DuongKhangDEV.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DuongKhangDEV.Application.ViewModels.BlogCatalog
{
    public class BlogCategoryViewModel
    {
        public int Id { get; set; }

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
    }
}
