using System;
using System.Collections.Generic;
using System.Text;

namespace DuongKhangDEV.Data.Interfaces
{
    public interface IHasOwner<T>
    {
        T OwnerId { set; get; }


    }
}
