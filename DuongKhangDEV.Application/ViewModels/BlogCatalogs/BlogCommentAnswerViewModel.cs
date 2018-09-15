using DuongKhangDEV.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DuongKhangDEV.Application.ViewModels.BlogCatalog
{
    public class BlogCommentAnswerViewModel
    {
        [Required]
        public int Id { get; set; }

        [StringLength(500)]
        public string Content { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { set; get; }

        public Status Status { set; get; }

        public Guid? AnswerId { set; get; }

        [Required]
        public int BlogCommentId { get; set; }
    }
}
