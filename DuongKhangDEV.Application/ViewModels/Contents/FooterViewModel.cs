using DuongKhangDEV.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DuongKhangDEV.Application.ViewModels.Content
{
    public class FooterViewModel
    {
        [Required]
        public string Id { set; get; }

        [Required]
        public string Content { set; get; }

        public Status Status { set; get; }
    }
}
