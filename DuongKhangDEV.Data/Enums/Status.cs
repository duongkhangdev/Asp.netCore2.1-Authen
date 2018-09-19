using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DuongKhangDEV.Data.Enums
{
    public enum Status
    {
        [Description("Khoá")]
        InActive = 0,

        [Description("Kích hoạt")]
        Active = 1,

        [Description("Tất cả")]
        None = 2
    }
}
