using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using DuongKhangDEV.Data.Enums;
using DuongKhangDEV.Data.Interfaces;
using DuongKhangDEV.Infrastructure.SharedKernel;

namespace DuongKhangDEV.Data.Entities.SystemCatalog
{
    [Table("Functions")]
    public class Function : DomainEntity<string>, ISwitchable, ISortable
    {
        public Function()
        {

        }

        public Function(string id, string name, string url, string parentId, string iconCss, int sortOrder, Status status)
        {
            Id = id;
            this.Name = name;
            this.URL = url;
            this.ParentId = parentId;
            this.IconCss = iconCss;
            this.SortOrder = sortOrder;
            this.Status = status;
        }

        [Required]
        [StringLength(255)]
        public string Name { set; get; }

        [Required]
        [StringLength(255)]
        public string URL { set; get; }

        [StringLength(100)]
        public string ParentId { set; get; }

        public string IconCss { get; set; }

        public int SortOrder { set; get; }

        public Status Status { set; get; }

        public virtual ICollection<Permission> Permissions { set; get; }
    }
}
