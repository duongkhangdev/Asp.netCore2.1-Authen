using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using DuongKhangDEV.Data.Entities.ProductCatalog;
using DuongKhangDEV.Infrastructure.SharedKernel;

namespace DuongKhangDEV.Data.Entities.ECommerce
{
    [Table("BillDetails")]
    public class BillDetail : DomainEntity<int>
    {
        #region Constructors

        public BillDetail()
        {

        }

        public BillDetail(int id, int billId, long productId, int quantity, decimal price)
        {
            Id = id;
            BillId = billId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }

        public BillDetail(int billId, long productId, int quantity, decimal price)
        {
            BillId = billId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }

        #endregion

        public int BillId { set; get; }

        public long ProductId { set; get; }

        public int Quantity { set; get; }

        public decimal Price { set; get; }

        [ForeignKey("BillId")]
        public virtual Bill Bill { set; get; }

        [ForeignKey("ProductId")]
        public virtual Product Product { set; get; }
    }
}
