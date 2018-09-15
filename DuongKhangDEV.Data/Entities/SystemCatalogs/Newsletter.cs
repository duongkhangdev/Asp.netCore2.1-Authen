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
    [Table("Newsletters")]
    public class Newsletter: DomainEntity<int>, ISwitchable, IDateTracking
    {
        #region Constructors

        public Newsletter()
        {

        }

        public Newsletter(int id, string email, Status status)
        {
            Id = id;
            Email = email;
            Status = status;

            if (id == 0)
            {
                DateCreated = DateTime.Now;
            }
            else
            {
                DateModified = DateTime.Now;
            }
        }

        #endregion

        [EmailAddress]
        [StringLength(255)]
        [Required]
        public string Email { set; get; }

        public Status Status { set; get; }

        public DateTime DateCreated { set; get; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { set; get; }
    }
}
