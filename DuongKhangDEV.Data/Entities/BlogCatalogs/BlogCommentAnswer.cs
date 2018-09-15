using DuongKhangDEV.Data.Enums;
using DuongKhangDEV.Data.Interfaces;
using DuongKhangDEV.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DuongKhangDEV.Data.Entities.BlogCatalog
{
    [Table("BlogCommentAnswers")]
    public class BlogCommentAnswer : DomainEntity<int>, ISwitchable, IDateTracking
    {
        #region Constructors

        public BlogCommentAnswer()
        {
            BlogComment = new BlogComment();
        }

        public BlogCommentAnswer(int id, int blogCommentId, string content, Status status, Guid? answerId)
        {
            Id = id;
            BlogCommentId = blogCommentId;
            Content = content;
            Status = status;
            AnswerId = answerId;
        }

        #endregion

        [StringLength(500)]
        public string Content { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { set; get; }

        public Status Status { set; get; }

        public Guid? AnswerId { set; get; }

        [Required]
        public int BlogCommentId { get; set; }

        [ForeignKey("BlogCommentId")]
        public virtual BlogComment BlogComment { set; get; }
    }
}
