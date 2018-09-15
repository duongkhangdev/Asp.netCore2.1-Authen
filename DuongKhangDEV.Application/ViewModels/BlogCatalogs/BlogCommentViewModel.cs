using DuongKhangDEV.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DuongKhangDEV.Application.ViewModels.BlogCatalog
{
    public class BlogCommentViewModel
    {
        [Required]
        public int Id { get; set; }

        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [EmailAddress]
        [StringLength(255)]
        [Required]
        public string Email { get; set; }

        [StringLength(500)]
        public string Content { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { set; get; }

        public Status Status { set; get; }

        public Guid? CustomerId { set; get; }

        [Required]
        public int BlogId { get; set; }

        public List<BlogCommentAnswerViewModel> BlogCommentAnswers { set; get; }
    }
}
