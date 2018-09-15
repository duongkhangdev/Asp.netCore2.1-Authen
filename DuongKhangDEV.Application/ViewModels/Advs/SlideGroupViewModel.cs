using DuongKhangDEV.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DuongKhangDEV.Application.ViewModels.Advs
{
    public class SlideGroupViewModel
    {
        public int Id { set; get; }

        [Required]
        [MaxLength(255)]
        public string Name { set; get; }

        public Status Status { set; get; }
    }
}
