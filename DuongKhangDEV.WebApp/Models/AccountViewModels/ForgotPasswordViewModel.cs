using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DuongKhangDEV.WebApp.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "Bạn phải nhập", AllowEmptyStrings = false)]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
