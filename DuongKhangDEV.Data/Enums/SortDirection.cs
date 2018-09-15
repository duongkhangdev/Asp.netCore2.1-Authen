using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DuongKhangDEV.Data.Enums
{
    public enum SortDirection
    {
        [Description("Tăng dần")]
        Ascending = 1,

        [Description("Giảm dần")]
        Descending = 2,

        [Description("Không chọn")]
        None = 3
    }
}
