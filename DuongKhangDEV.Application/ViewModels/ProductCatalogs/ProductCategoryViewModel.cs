using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DuongKhangDEV.Data.Enums;

namespace DuongKhangDEV.Application.ViewModels.ProductCatalog
{
    public class ProductCategoryViewModel
    {
        [Required]
        public int Id { get; set; }

        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [StringLength(50)]
        public string Name2 { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int? ParentId { get; set; }

        public int? HomeOrder { get; set; }

        [StringLength(255)]
        public string ThumbnailImage { get; set; }

        [StringLength(50)]
        public string UniqueCode { get; set; }

        public bool? HomeFlag { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { set; get; }

        public int SortOrder { set; get; }

        public Status Status { set; get; }

        [StringLength(255)]
        public string MetaTitle { set; get; }

        [StringLength(255)]
        public string MetaAlias { set; get; }

        [StringLength(255)]
        public string MetaKeywords { set; get; }

        [StringLength(255)]
        public string MetaDescription { set; get; }

        public ICollection<ProductViewModel> Products { set; get; }
    }
}
