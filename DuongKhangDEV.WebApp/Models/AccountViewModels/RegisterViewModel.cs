using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DuongKhangDEV.WebApp.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Bạn phải nhập", AllowEmptyStrings = false)]
        [Display(Name = "Họ tên")]
        public string FullName { set; get; }

        [Display(Name = "Ngày sinh")]
        public DateTime? BirthDay { set; get; }
                
        [EmailAddress]
        [Required(ErrorMessage = "Bạn phải nhập", AllowEmptyStrings = false)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có 6 ký tự trở lên")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không đúng.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [Display(Name = "Tỉnh/Thành phố")]
        public string City { get; set; }

        [Display(Name = "Điện thoại")]
        public string PhoneNumber { set; get; }

        [Display(Name = "Ảnh đại diện")]
        public string Avatar { get; set; }
    }
}
