using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using DuongKhangDEV.Data.Entities.Commons;
using DuongKhangDEV.Infrastructure.SharedKernel;

namespace DuongKhangDEV.Data.Entities.BlogCatalog
{
    [Table("BlogTags")]
    public class BlogTag : DomainEntity<int>
    {
        #region Constructors

        public BlogTag()
        {

        }

        public BlogTag(int id, int blogId, string tagId)
        {
            Id = id;
            BlogId = blogId;
            TagId = tagId;
        }

        #endregion

        [Required]
        public int BlogId { set; get; }

        [Required]
        public string TagId { set; get; }

        [ForeignKey("BlogId")]
        public virtual Blog Blog { set; get; }

        [ForeignKey("TagId")]
        public virtual Tag Tag { set; get; }
    }
}
