using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using DuongKhangDEV.Data.Enums;
using DuongKhangDEV.Data.Interfaces;
using DuongKhangDEV.Infrastructure.SharedKernel;

namespace DuongKhangDEV.Data.Entities.Content
{
    [Table("Footers")]
    public class Footer : DomainEntity<string>, ISwitchable
    {
        #region Constructors

        public Footer()
        {

        }

        public Footer(string id, string content, Status status)
        {
            Id = id;
            Content = content;
            Status = status;
        }

        #endregion

        [Required]
        public string Content { set; get; }

        public Status Status { set; get; }
    }
}
