using System;
using System.Collections.Generic;
using System.Text;

namespace DuongKhangDEV.Data.Interfaces
{
    public interface IHasSoftDelete
    {
        bool IsDeleted { set; get; }
    }
}
