using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DuongKhangDEV.Application.ViewModels.BlogCatalog
{
    public class BlogInCategoryViewModel
    {
        public int Id { get; set; }

        public int? DisplayOrder { get; set; }

        [Required]
        public int BlogId { get; set; }

        [Required]
        public int BlogCategoryId { get; set; }
    }
}
