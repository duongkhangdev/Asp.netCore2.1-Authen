using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DuongKhangDEV.Application.ViewModels.ProductCatalog;
using DuongKhangDEV.Application.ViewModels.SystemCatalog;
using DuongKhangDEV.Data.Enums;

namespace DuongKhangDEV.Application.ViewModels.ECommerce
{
    public class BillDetailViewModel
    {
        public int Id { get; set; }

        public int BillId { set; get; }

        public long ProductId { set; get; }

        public int Quantity { set; get; }

        public decimal Price { set; get; }

        public BillViewModel Bill { set; get; }

        public ProductViewModel Product { set; get; }
    }
}
