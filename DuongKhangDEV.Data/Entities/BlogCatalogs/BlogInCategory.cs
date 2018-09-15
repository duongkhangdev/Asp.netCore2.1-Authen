using DuongKhangDEV.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DuongKhangDEV.Data.Entities.BlogCatalog
{
    [Table("BlogInCategories")]
    public class BlogInCategory : DomainEntity<int>
    {
        #region Constructors

        public BlogInCategory()
        {

        }

        public BlogInCategory(int id, int blogId, int blogCategoryId)
        {
            Id = id;
            BlogId = blogId;
            BlogCategoryId = blogCategoryId;
        }

        public BlogInCategory(int blogId, int blogCategoryId)
        {
            BlogId = blogId;
            BlogCategoryId = blogCategoryId;
        }

        #endregion

        public int? DisplayOrder { get; set; }

        [Required]
        public int BlogId { get; set; }

        [Required]
        public int BlogCategoryId { get; set; }

        [ForeignKey("BlogId")]
        public virtual Blog Blog { set; get; }

        [ForeignKey("BlogCategoryId")]
        public virtual BlogCategory BlogCategory { set; get; }
    }
}
