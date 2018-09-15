using System;
using System.Collections.Generic;
using System.Text;

namespace DuongKhangDEV.Data.Interfaces
{
    public interface IDateTracking
    {
        DateTime DateCreated { set; get; }

        DateTime? DateModified { set; get; }
    }
}
