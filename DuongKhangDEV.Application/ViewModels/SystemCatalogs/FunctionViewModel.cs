using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DuongKhangDEV.Data.Enums;

namespace DuongKhangDEV.Application.ViewModels.SystemCatalog
{
    public class FunctionViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { set; get; }

        [Required]
        [StringLength(255)]
        public string URL { set; get; }
        
        public string ParentId { set; get; }

        public string IconCss { get; set; }
        public int SortOrder { set; get; }
        public Status Status { set; get; }
    }
}
