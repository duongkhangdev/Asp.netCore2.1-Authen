using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using DuongKhangDEV.Data.Enums;
using DuongKhangDEV.Data.Interfaces;
using DuongKhangDEV.Infrastructure.SharedKernel;

namespace DuongKhangDEV.Data.Entities
{
    [Table("Pages")]
    public class Page : DomainEntity<int>, ISwitchable, IHasSeoMetaData
    {
        #region Constructors

        public Page()
        {

        }

        public Page(int id, string name, string alias, string target,
            string content, Status status,
            string metaTitle, string metaAlias, string metaKeyword, string metaDescription)
        {
            Id = id;
            Name = name;
            Alias = alias;
            Target = target;
            Content = content;
            Status = status;
            MetaTitle = metaTitle;
            MetaAlias = metaAlias;
            MetaKeywords = metaKeyword;
            MetaDescription = metaDescription;
        }

        #endregion

        [Required]
        [MaxLength(255)]
        public string Name { set; get; }

        [MaxLength(255)]
        [Required]
        public string Alias { set; get; }

        [StringLength(25)]
        [Required]
        public string Target { get; set; }

        public string Content { set; get; }

        public Status Status { set; get; }

        [MaxLength(255)]
        public string MetaTitle { set; get; }

        [MaxLength(255)]
        public string MetaAlias { set; get; }

        [MaxLength(255)]
        public string MetaKeywords { set; get; }

        [MaxLength(255)]
        public string MetaDescription { set; get; }
    }
}
