using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DuongKhangDEV.Application.Interfaces.ProductCatalogs;
using DuongKhangDEV.Application.ViewModels.ProductCatalog;
using DuongKhangDEV.Data.Entities.ProductCatalog;
using DuongKhangDEV.Utilities.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DuongKhangDEV.WebApp.Areas.Admin.Controllers.ProductCatalogs
{
    public class ProductCategoryController : BaseController
    {
        IProductCategoryService _productCategoryService;
        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var model = await _productCategoryService.GetAllAsync();
            return new OkObjectResult(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPagingAsync(string keyword, int status, int page, int pageSize)
        {
            Expression<Func<ProductCategory, bool>> myExpression;
            if (!String.IsNullOrEmpty(keyword))
            {
                if (status < 0)
                {
                    myExpression = i => i.Status == (Data.Enums.Status)status;
                }

                myExpression = i => i.Name.Contains(keyword);

                var model = await _productCategoryService.GetAllPagingAsync(myExpression, page, pageSize);
                return new OkObjectResult(model);
            }
            else
            {
                if (status < 0)
                {
                    var model = await _productCategoryService.GetAllPagingAsync(page, pageSize);
                    return new OkObjectResult(model);
                }
                else
                {
                    myExpression = i => i.Status == (Data.Enums.Status)status;

                    var model = await _productCategoryService.GetAllPagingAsync(myExpression, page, pageSize);
                    return new OkObjectResult(model);
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var model = await _productCategoryService.GetByIdAsync(id);

            return new OkObjectResult(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveEntity(ProductCategoryViewModel productCategoryVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            else
            {
                if (String.IsNullOrEmpty(productCategoryVm.MetaAlias))
                {
                    productCategoryVm.MetaAlias = TextHelper.ToUnsignString(productCategoryVm.Name);
                }
                if (productCategoryVm.Id == 0)
                {
                    await _productCategoryService.InsertAsync(productCategoryVm);
                }
                else
                {
                    await _productCategoryService.UpdateAsync(productCategoryVm);
                }

                return new OkObjectResult(productCategoryVm);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                await _productCategoryService.DeleteAsync(id);

                return new OkObjectResult(id);
            }
        }
    }
}