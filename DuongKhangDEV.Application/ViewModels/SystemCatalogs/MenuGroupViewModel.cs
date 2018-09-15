using DuongKhangDEV.Data.Entities.SystemCatalog;
using DuongKhangDEV.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DuongKhangDEV.Application.ViewModels.SystemCatalog
{
    public class MenuGroupViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { set; get; }

        public Status Status { set; get; }

        public virtual IEnumerable<Menu> Menus { set; get; }
    }
}
