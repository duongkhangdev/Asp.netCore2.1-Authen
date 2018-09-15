using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DuongKhangDEV.Application.ViewModels.SystemCatalog;
using DuongKhangDEV.Data.Enums;

namespace DuongKhangDEV.Application.ViewModels.ECommerce
{
    public class BillViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string CustomerName { set; get; }

        [Required]
        [MaxLength(256)]
        public string CustomerAddress { set; get; }

        [Required]
        [MaxLength(20)]
        public string CustomerMobile { set; get; }

        [MaxLength(255)]
        public string CustomerEmail { set; get; }

        [MaxLength(255)]
        public string CustomerZalo { set; get; }

        [MaxLength(255)]
        public string CustomerFacebook { set; get; }

        [Required]
        [MaxLength(256)]
        public string CustomerMessage { set; get; }

        public PaymentMethod PaymentMethod { set; get; }

        public BillStatus BillStatus { set; get; }

        public DateTime DateCreated { set; get; }

        public DateTime? DateModified { set; get; }

        public DateTime? DateDeleted { set; get; }

        public Status Status { set; get; }

        public Guid? CustomerId { set; get; }

        public List<BillDetailViewModel> BillDetails { set; get; }
    }
}
