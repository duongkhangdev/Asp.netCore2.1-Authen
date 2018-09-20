using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DuongKhangDEV.Application.Interfaces.ProductCatalogs;
using DuongKhangDEV.Application.ViewModels.ProductCatalog;
using DuongKhangDEV.Data.Entities.ProductCatalog;
using DuongKhangDEV.Utilities.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OfficeOpenXml;
using OfficeOpenXml.Table;

namespace DuongKhangDEV.WebApp.Areas.Admin.Controllers.ProductCatalogs
{
    public class ProductController : BaseController
    {
        private IProductService _productService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ProductController(IProductService productService,
            IHostingEnvironment hostingEnvironment)
        {
            _productService = productService;
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
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var model = await _productService.GetByIdAsync(id);

            return new OkObjectResult(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveEntityAsync(ProductViewModel productVm)
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
        public async Task<IActionResult> DeleteAsync(long id)
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
        public async Task<IActionResult> DeleteRangeAsync(long[] id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                await _productService.DeleteRangeAsync(id);

                return new OkObjectResult(id);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveImagesAsync(long productId, string[] images)
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
        public async Task<IActionResult> GetImagesAsync(long productId)
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

        [HttpPost]
        public async Task<IActionResult> ImportExcelAsync(IList<IFormFile> files, int categoryId)
        {
            if (files != null && files.Count > 0)
            {
                var file = files[0];
                var filename = ContentDispositionHeaderValue
                                   .Parse(file.ContentDisposition)
                                   .FileName
                                   .Trim('"');

                string folder = _hostingEnvironment.WebRootPath + $@"\uploaded\excels";
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                string filePath = Path.Combine(folder, filename);

                using (FileStream fs = System.IO.File.Create(filePath))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
                await _productService.ImportExcelAsync(filePath, categoryId);
                
                return new OkObjectResult(filePath);
            }
            return new NoContentResult();
        }

        [HttpPost]
        public async Task<IActionResult> ExportExcelAsync()
        {
            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string directory = Path.Combine(sWebRootFolder, "export-files");
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string sFileName = $"Product_{DateTime.Now:yyyyMMddhhmmss}.xlsx";
            string fileUrl = $"{Request.Scheme}://{Request.Host}/export-files/{sFileName}";
            FileInfo file = new FileInfo(Path.Combine(directory, sFileName));
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            }

            var products = await _productService.GetAllAsync();

            using (ExcelPackage package = new ExcelPackage(file))
            {
                // add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Products");
                worksheet.Cells["A1"].LoadFromCollection(products, true, TableStyles.Light1);
                worksheet.Cells.AutoFitColumns();
                package.Save(); //Save the workbook.
            }
            return new OkObjectResult(fileUrl);
        }
    }
}