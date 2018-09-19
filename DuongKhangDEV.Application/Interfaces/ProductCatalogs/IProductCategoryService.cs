using DuongKhangDEV.Application.ViewModels.ProductCatalog;
using DuongKhangDEV.Data.Entities.ProductCatalog;
using System;
using System.Collections.Generic;
using System.Text;

namespace DuongKhangDEV.Application.Interfaces.ProductCatalogs
{
    public interface IProductCategoryService : IWebServiceBase<ProductCategory, int, ProductCategoryViewModel>
    {

    }
}
