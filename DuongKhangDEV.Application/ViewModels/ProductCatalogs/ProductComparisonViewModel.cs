using System;
using System.Collections.Generic;
using System.Text;

namespace DuongKhangDEV.Application.ViewModels.ProductCatalog
{
    public class ProductComparisonViewModel
    {
        public int Id { get; set; }

        public long ProductId { get; set; }

        public Guid UserId { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public DateTime? DateDeleted { set; get; }
    }
}
