using System;
using System.Collections.Generic;
using System.Text;

namespace DuongKhangDEV.Application.ViewModels.ProductCatalog
{
    public class ProductImageViewModel
    {
        public long Id { get; set; }

        public long ProductId { get; set; }

        public ProductViewModel Product { get; set; }

        public string Path { get; set; }

        public string Caption { get; set; }
    }
}
