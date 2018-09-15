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
    [Table("BlogComments")]
    public class BlogComment: DomainEntity<int>, ISwitchable, IDateTracking
    {
        #region Constructors

        public BlogComment()
        {
            Blog = new Blog();
            BlogCommentAnswer = new List<BlogCommentAnswer>();
        }

        public BlogComment(int id, int blogId, string name, string email, string content, Status status, Guid? customerId)
        {
            Id = id;
            BlogId = blogId;
            Name = name;
            Email = email;
            Content = content;
            Status = status;
            CustomerId = customerId;
        }

        #endregion

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

        [ForeignKey("BlogId")]
        public virtual Blog Blog { set; get; }

        public virtual ICollection<BlogCommentAnswer> BlogCommentAnswer { set; get; }
    }
}
