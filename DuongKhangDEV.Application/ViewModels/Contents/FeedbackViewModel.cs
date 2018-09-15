using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DuongKhangDEV.Data.Enums;

namespace DuongKhangDEV.Application.ViewModels.Content
{
    public class FeedbackViewModel
    {
        public int Id { set; get; }

        [Required]
        [StringLength(255)]
        public string Name { set; get; }

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { set; get; }

        [Required]
        [StringLength(20)]
        public string Phone { set; get; }

        [Required]
        [StringLength(500)]
        public string Message { set; get; }

        public Status Status { set; get; }

        public DateTime DateCreated { set; get; }

        public DateTime DateModified { set; get; }

        public DateTime? DateDeleted { set; get; }
    }
}
