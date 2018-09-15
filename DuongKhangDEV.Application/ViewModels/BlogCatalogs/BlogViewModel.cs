using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DuongKhangDEV.Data.Enums;

namespace DuongKhangDEV.Application.ViewModels.BlogCatalog
{
    public class BlogViewModel
    {
        public int Id { set; get; }

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
        
        public DateTime DateCreated { set; get; }

        public DateTime? DateModified { set; get; }

        public DateTime? DateDeleted { set; get; }

        public Status Status { set; get; }

        [MaxLength(255)]
        public string MetaTitle { set; get; }

        [MaxLength(255)]
        public string MetaAlias { set; get; }

        [MaxLength(2565)]
        public string MetaKeywords { set; get; }

        [MaxLength(255)]
        public string MetaDescription { set; get; }

        public List<BlogTagViewModel> BlogTags { set; get; }

        public List<BlogCommentViewModel> BlogComments { set; get; }
    }
}
