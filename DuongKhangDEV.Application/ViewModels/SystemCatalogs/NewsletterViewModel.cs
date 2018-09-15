using DuongKhangDEV.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DuongKhangDEV.Application.ViewModels.SystemCatalog
{
    public class NewsletterViewModel
    {
        [Required]
        public int Id { set; get; }

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
