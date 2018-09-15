using System;
using System.Collections.Generic;
using System.Text;

namespace DuongKhangDEV.Application.ViewModels.ProductCatalog
{
    public class WholePriceViewModel
    {
        public long ProductId { get; set; }

        public int FromQuantity { get; set; }

        public int ToQuantity { get; set; }

        public decimal Price { get; set; }
    }
}
