using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using DuongKhangDEV.Data.Interfaces;
using DuongKhangDEV.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DuongKhangDEV.Data.Entities.SystemCatalog
{
    [Table("AppUsers")]
    public class AppUser : IdentityUser<Guid>, IDateTracking, ISwitchable
    {
        #region Constructors

        public AppUser()
        {

        }

        public AppUser(Guid id, string fullName, string userName, 
            string email, string phoneNumber, string address, string city, string avatar, Status status)
        {
            Id = id;
            FullName = fullName;
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            City = city;
            Avatar = avatar;
            Status = status;

            if (id == null)
            {
                DateCreated = DateTime.Now;
            }
            else
            {
                DateModified = DateTime.Now;
            }
        }

        #endregion

        [StringLength(255)]
        public string FullName { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        public DateTime? BirthDay { set; get; }

        public decimal Balance { get; set; }

        public string Avatar { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { set; get; }

        public Status Status { get; set; }
    }
}
