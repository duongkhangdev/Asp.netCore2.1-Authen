using DuongKhangDEV.Application.Interfaces.ProductCatalogs;
using DuongKhangDEV.Application.ViewModels.ProductCatalog;
using DuongKhangDEV.Data.Entities.ProductCatalog;
using DuongKhangDEV.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DuongKhangDEV.Application.Implementation.ProductCatalogs
{
    public class ProductCategoryService : WebServiceBase<ProductCategory, int, ProductCategoryViewModel>, IProductCategoryService
    {
        private readonly IRepository<ProductCategory, int> _productCategoryService;

        public ProductCategoryService(IRepository<ProductCategory, int> productCategoryService,
            IUnitOfWork unitOfWork) : base(productCategoryService, unitOfWork)
        {
            _productCategoryService = productCategoryService;
        }
    }
}
