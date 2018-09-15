using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using DuongKhangDEV.Data.Enums;
using DuongKhangDEV.Infrastructure.SharedKernel;

namespace DuongKhangDEV.Data.Entities.Content
{
    [Table("ContactDetails")]
    public class Contact : DomainEntity<string>
    {
        #region Constructors

        public Contact()
        {

        }

        public Contact(string id, string name, string phone, string email, string zalo, string facebook, string skype,
            string website, string address, string other, double? longtitude, double? latitude, Status status)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Email = email;
            Zalo = zalo;
            Facebook = facebook;
            Skype = skype;
            Website = website;
            Address = address;
            Other = other;
            Lng = longtitude;
            Lat = latitude;
            Status = status;
        }

        #endregion

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

        public double? Lat { set; get; } // Vĩ độ

        public double? Lng { set; get; } // Kinh độ

        public Status Status { set; get; }
    }
}
