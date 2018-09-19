using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DuongKhangDEV.Application.Interfaces.ProductCatalogs;
using DuongKhangDEV.Application.ViewModels.ProductCatalog;
using DuongKhangDEV.Data.Entities.ProductCatalog;
using DuongKhangDEV.Utilities.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DuongKhangDEV.WebApp.Areas.Admin.Controllers.ProductCatalogs
{
    public class ProductController : BaseController
    {
        private IProductService _productService;
        private IProductCategoryService _productCategoryService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ProductController(IProductService productService,
            IProductCategoryService productCategoryService,
            IHostingEnvironment hostingEnvironment)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var model = await _productService.GetAllAsync();
            return new OkObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var model = _productService.GetAll();
            return new OkObjectResult(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var model = await _productCategoryService.GetAllAsync();
            return new OkObjectResult(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPagingAsync(string keyword, int? categoryId, int status, int page, int pageSize)
        {
            Expression<Func<Product, bool>> myExpression;
            if (!String.IsNullOrEmpty(keyword))
            {
                if (status < 0)
                {
                    myExpression = i => i.Status == (Data.Enums.Status)status;
                }

                myExpression = i => i.Name.Contains(keyword);

                if (categoryId != null)
                {
                    myExpression = i => i.CategoryId == categoryId;
                }

                var model = await _productService.GetAllPagingAsync(myExpression, page, pageSize);
                return new OkObjectResult(model);
            }
            else
            {
                if (status < 0)
                {
                    if (categoryId != null)
                    {
                        myExpression = i => i.CategoryId == categoryId;
                        var model = await _productService.GetAllPagingAsync(myExpression, page, pageSize);
                        return new OkObjectResult(model);
                    }
                    else
                    {
                        var model = await _productService.GetAllPagingAsync(page, pageSize);
                        return new OkObjectResult(model);
                    }                    
                }
                else
                {
                    myExpression = i => i.Status == (Data.Enums.Status)status;

                    if (categoryId != null)
                    {
                        myExpression = i => i.CategoryId == categoryId;
                    }
                    var model = await _productService.GetAllPagingAsync(myExpression, page, pageSize);                    
                    return new OkObjectResult(model);
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPagingOrderByAsync(string keyword, string orderBy, int sortDirection, int status, int pageIndex, int pageSize)
        {
            Expression<Func<Product, object>> sortExpression;
            switch (orderBy)
            {
                case "Name":
                    sortExpression = (x => x.Name);
                    break;
                case "DateCreated":
                    sortExpression = (x => x.DateCreated);
                    break;
                default:
                    sortExpression = (x => x.Id);
                    break;
            }

            Expression<Func<Product, bool>> myExpression;
            if (!String.IsNullOrEmpty(keyword))
            {
                myExpression = i => i.Name.Contains(keyword);
            }

            myExpression = i => i.Status == (Data.Enums.Status)status;

            var model = await _productService.GetAllPagingAsync(myExpression, sortExpression, (Data.Enums.SortDirection)sortDirection, pageIndex, pageSize);
            return new OkObjectResult(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(long id)
        {
            var model = await _productService.GetByIdAsync(id);

            return new OkObjectResult(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveEntity(ProductViewModel productVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            else
            {
                if (String.IsNullOrEmpty(productVm.MetaAlias))
                {
                    productVm.MetaAlias = TextHelper.ToUnsignString(productVm.Name);
                }
                if (productVm.Id == 0)
                {
                    await _productService.InsertAsync(productVm);
                }
                else
                {
                    await _productService.UpdateAsync(productVm);
                }
                
                return new OkObjectResult(productVm);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                await _productService.DeleteAsync(id);                

                return new OkObjectResult(id);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveImages(long productId, string[] images)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                var results = await _productService.AddImagesAsync(productId, images);

                return new OkObjectResult(results);
            }            
        }

        [HttpGet]
        public async Task<IActionResult> GetImages(long productId)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                var images = await _productService.GetImagesAsync(productId);
                return new OkObjectResult(images);
            }            
        }
    }
}