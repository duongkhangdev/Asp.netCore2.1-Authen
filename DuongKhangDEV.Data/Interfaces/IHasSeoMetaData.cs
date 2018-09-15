using System;
using System.Collections.Generic;
using System.Text;

namespace DuongKhangDEV.Data.Interfaces
{
    public interface IHasSeoMetaData
    {
        string MetaTitle { set; get; }

        string MetaAlias { set; get; }

        string MetaKeywords { set; get; }

        string MetaDescription { get; set; }
    }
}
