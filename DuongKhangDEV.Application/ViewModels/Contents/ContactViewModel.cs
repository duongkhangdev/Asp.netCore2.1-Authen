using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DuongKhangDEV.Data.Enums;

namespace DuongKhangDEV.Application.ViewModels.Content
{
    public class ContactViewModel
    {
        public string Id { set; get; }

        [StringLength(255)]
        [Required]
        public string Name { set; get; }

        [StringLength(20)]
        public string Phone { set; get; }

        [EmailAddress]
        [StringLength(255)]
        [Required]
        public string Email { set; get; }

        [StringLength(255)]
        public string Zalo { set; get; }

        [StringLength(255)]
        public string Facebook { set; get; }

        [StringLength(255)]
        public string Skype { set; get; }

        [StringLength(255)]
        public string Website { set; get; }

        [StringLength(255)]
        public string Address { set; get; }

        public string Other { set; get; }

        public double? Lat { set; get; }

        public double? Lng { set; get; }

        public Status Status { set; get; }
    }
}
