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
    [Table("Feedbacks")]
    public class Feedback : DomainEntity<int>, ISwitchable, IDateTracking
    {
        #region Constructors

        public Feedback()
        {

        }

        public Feedback(int id, string name, string email, string phone, string message, Status status)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
            Message = message;
            Status = status;
        }

        #endregion

        [StringLength(255)]
        [Required]
        public string Name { set; get; }

        [EmailAddress]
        [StringLength(255)]
        [Required]
        public string Email { set; get; }

        [Required]
        [StringLength(20)]
        public string Phone { set; get; }

        [Required]
        [StringLength(500)]
        public string Message { set; get; }

        public Status Status { set; get; }

        public DateTime DateCreated { set; get; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { set; get; }
    }
}
