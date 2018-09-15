using DuongKhangDEV.Data.Enums;
using DuongKhangDEV.Data.Interfaces;
using DuongKhangDEV.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DuongKhangDEV.Data.Entities.SystemCatalog
{
    [Table("Menus")]
    public class Menu : DomainEntity<int>, ISwitchable
    {
        #region Constructors

        public Menu()
        {

        }

        public Menu(int id, string name, int? parentId, int? homeOrder, string url, string target, string thumbnailImage, string description, string uniqueCode, int? displayOrder, Status status, int groupId)
        {
            Id = id;
            Name = name;
            ParentId = parentId;
            HomeOrder = homeOrder;
            Url = url;
            Target = target;
            ThumbnailImage = thumbnailImage;
            Description = description;
            UniqueCode = uniqueCode;
            DisplayOrder = displayOrder;            
            Status = status;
            GroupId = groupId;
        }

        #endregion

        [Required]
        [MaxLength(255)]
        public string Name { set; get; }

        public int? ParentId { set; get; }

        public int? HomeOrder { get; set; }

        [Required]
        [MaxLength(500)]
        public string Url { set; get; }

        [MaxLength(25)]
        public string Target { set; get; }

        [MaxLength(255)]
        public string ThumbnailImage { set; get; }

        [Required]
        [MaxLength(500)]
        public string Description { set; get; }

        [StringLength(50)]
        public string UniqueCode { get; set; }

        public int? DisplayOrder { set; get; }

        public Status Status { set; get; }

        [Required]
        public int GroupId { set; get; }

        [ForeignKey("GroupId")]
        public virtual MenuGroup MenuGroup { set; get; }
    }
}
