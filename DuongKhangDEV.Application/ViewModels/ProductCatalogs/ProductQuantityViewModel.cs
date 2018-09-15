using System;
using System.Collections.Generic;
using System.Text;

namespace DuongKhangDEV.Application.ViewModels.ProductCatalog
{
    public class ProductQuantityViewModel
    {
        public long ProductId { get; set; }

        public int Quantity { get; set; }

        public ProductViewModel Product { get; set; }
    }
}
