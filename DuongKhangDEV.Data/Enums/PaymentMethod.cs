using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DuongKhangDEV.Data.Enums
{
    public enum PaymentMethod
    {
        [Description("Thanh toán khi nhận hàng (COD)")]
        CashOnDelivery = 1,

        [Description("Chuyển khoản Internet Banking")]
        OnlineBanking = 2,

        [Description("Thanh toán tại cửa hàng")]
        CashOnHome = 3,

        [Description("Chuyển khoản ATM")]
        Atm = 4
    }
}
