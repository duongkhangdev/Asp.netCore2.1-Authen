using DuongKhangDEV.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DuongKhangDEV.Data.Entities.SystemCatalog
{
    [Table("VisitorStatistics")]
    public class VisitorStatistic: DomainEntity<Guid>
    {
        #region Constructors

        public VisitorStatistic()
        {

        }

        public VisitorStatistic(Guid id, DateTime visitedDate, string ipAddress)
        {
            Id = id;
            VisitedDate = visitedDate;
            IPAddress = ipAddress;
        }

        #endregion

        [Required]
        public DateTime VisitedDate { set; get; }

        [MaxLength(50)]
        public string IPAddress { set; get; }
    }
}
