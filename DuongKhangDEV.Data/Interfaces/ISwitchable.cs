using System;
using System.Collections.Generic;
using System.Text;
using DuongKhangDEV.Data.Enums;

namespace DuongKhangDEV.Data.Interfaces
{
    public interface ISwitchable
    {
        Status Status { set; get; }
    }
}
