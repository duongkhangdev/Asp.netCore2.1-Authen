using DuongKhangDEV.Data.Enums;
using DuongKhangDEV.Data.Interfaces;
using DuongKhangDEV.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DuongKhangDEV.Data.Entities.Advs
{
    [Table("BannerGroups")]
    public class BannerGroup : DomainEntity<int>, ISwitchable
    {
        #region Constructors

        public BannerGroup()
        {
            Banners = new List<Banner>();
        }

        public BannerGroup(int id, string name, Status status)
        {
            Id = id;
            Name = name;
            Status = status;
        }

        #endregion

        [Required]
        [MaxLength(255)]
        public string Name { set; get; }

        public Status Status { set; get; }

        public virtual ICollection<Banner> Banners { set; get; }
    }
}
