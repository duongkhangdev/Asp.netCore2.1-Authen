using AutoMapper;
using AutoMapper.QueryableExtensions;
using DuongKhangDEV.Application.Interfaces.ProductCatalogs;
using DuongKhangDEV.Application.ViewModels.Common;
using DuongKhangDEV.Application.ViewModels.ProductCatalog;
using DuongKhangDEV.Data.Entities.Commons;
using DuongKhangDEV.Data.Entities.ProductCatalog;
using DuongKhangDEV.Data.Enums;
using DuongKhangDEV.Infrastructure.Interfaces;
using DuongKhangDEV.Utilities.Constants;
using DuongKhangDEV.Utilities.Dtos;
using DuongKhangDEV.Utilities.Helpers;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuongKhangDEV.Application.Implementation.ProductCatalogs
{
    public class ProductService : WebServiceBase<Product, long, ProductViewModel>, IProductService
    {
        private readonly IRepository<Product, long> _productService;
        private readonly IRepository<ProductImage, long> _productImageService;
        private readonly IRepository<ProductTag, int> _productTagService;
        private readonly IRepository<Tag, string> _tagService;

        public ProductService(IRepository<Product, long> productService, 
            IRepository<ProductImage, long> productImageService,
            IRepository<ProductTag, int> productTagService,
            IRepository<Tag, string> tagService,
            IUnitOfWork unitOfWork) : base(productService, unitOfWork)
        {
            _productService = productService;
            _productImageService = productImageService;
            _productTagService = productTagService;
            _tagService = tagService;
        }

        public override async Task<ProductViewModel> InsertAsync(ProductViewModel productVm)
        {
            var product = Mapper.Map<ProductViewModel, Product>(productVm);

            if (!string.IsNullOrEmpty(productVm.Tags))
            {
                string[] tags = productVm.Tags.Split(',');
                foreach (string t in tags)
                {
                    var tagId = TextHelper.ToUnsignString(t);
                    if (!_tagService.GetAll().Where(x => x.Id == tagId).Any())
                    {
                        Tag tag = new Tag
                        {
                            Id = tagId,
                            Name = t,
                            Type = CommonConstants.ProductTag
                        };
                        await _tagService.InsertAsync(tag);
                    }

                    ProductTag productTag = new ProductTag
                    {
                        TagId = tagId
                    };
                    await _productTagService.InsertAsync(productTag);
                }
            }

            var result = await _productService.InsertAsync(product);

            return Mapper.Map<Product, ProductViewModel>(result);
        }

        public override async Task<ProductViewModel> UpdateAsync(ProductViewModel productVm)
        {
            var product = Mapper.Map<ProductViewModel, Product>(productVm);
            var item = await _productTagService.GetAllAsync(x => x.ProductId == product.Id);
            await _productTagService.DeleteRangeAsync(item);

            if (!string.IsNullOrEmpty(productVm.Tags))
            {
                string[] tags = productVm.Tags.Split(',');
                foreach (string t in tags)
                {
                    var tagId = TextHelper.ToUnsignString(t);
                    if (!_tagService.GetAll().Where(x => x.Id == tagId).Any())
                    {
                        Tag tag = new Tag
                        {
                            Id = tagId,
                            Name = t,
                            Type = CommonConstants.ProductTag
                        };
                        await _tagService.InsertAsync(tag);
                    }

                    ProductTag productTag = new ProductTag
                    {
                        TagId = tagId
                    };
                    await _productTagService.InsertAsync(productTag);
                }
            }
           
            var result = await _productService.UpdateAsync(product);

            return Mapper.Map<Product, ProductViewModel>(result);
        }

        /// <summary>
        /// Danh sách sản phẩm mới nhất
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public async Task<List<ProductViewModel>> GetLastestAsync(int top)
        {
            var query = _productService.GetAll(x => x.Status == Status.Active)
                .OrderByDescending(x => x.DateCreated)
                .Take(top)
                .ProjectTo<ProductViewModel>()
                .ToListAsync();
            return await query;
        }

        /// <summary>
        /// Danh sách Sản phẩm nổi bật (Featured Products)
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public async Task<List<ProductViewModel>> GetHotProductAsync(int top)
        {
            var query = _productService.GetAll(x => x.Status == Status.Active && x.HotFlag == true)
                .OrderByDescending(x => x.DateCreated)
                .Take(top)
                .ProjectTo<ProductViewModel>()
                .ToListAsync();
            return await query;
        }

        /// <summary>
        /// Danh sách sản phẩm liên quan
        /// </summary>
        /// <param name="id"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public async Task<List<ProductViewModel>> GetRelatedProductAsync(long id, int top)
        {
            var product = _productService.GetById(id);
            var query = _productService.GetAll(x => x.Status == Status.Active
                && x.Id != id && x.CategoryId == product.CategoryId)
            .OrderByDescending(x => x.DateCreated)
            .Take(top)
            .ProjectTo<ProductViewModel>()
            .ToListAsync();
            return await query;
        }

        /// <summary>
        /// Danh sách sản phẩm khuyến mãi
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public async Task<List<ProductViewModel>> GetUpsellProductAsync(int top)
        {
            var query = _productService.GetAll(x => x.PromotionPrice != null)
               .OrderByDescending(x => x.DateModified)
               .Take(top)
               .ProjectTo<ProductViewModel>()
               .ToListAsync();
            return await query;
        }

        /// <summary>
        /// Danh sách sản phẩm khuyến mãi - Luôn hiển thị trên MainMenu
        /// </summary>
        /// <param name="top"></param>
        /// <param name="showOnMainMenu"></param>
        /// <returns></returns>
        public async Task<List<ProductViewModel>> GetUpsellProductAsync(int top, bool showOnMainMenu)
        {
            if (showOnMainMenu)
            {
                return await _productService.GetAll(x => x.PromotionPrice != null && x.AlwaysOnTheMainMenu == true)
               .OrderByDescending(x => x.DateModified)
               .Take(top)
               .ProjectTo<ProductViewModel>()
               .ToListAsync();
            }
            else
            {
                return await _productService.GetAll(x => x.PromotionPrice != null)
               .OrderByDescending(x => x.DateModified)
               .Take(top)
               .ProjectTo<ProductViewModel>()
               .ToListAsync();
            }            
        }

        /// <summary>
        /// Danh sách sản phẩm xem nhiều nhất (Popular Products)
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public async Task<List<ProductViewModel>> GetPopularProductAsync(int top)
        {
            return await _productService.GetAll(x => x.Status == Status.Active)
            .OrderByDescending(x => x.ViewCount)
            .OrderByDescending(x => x.DateCreated)
            .Take(top)
            .ProjectTo<ProductViewModel>()
            .ToListAsync();
        }

        /// <summary>
        /// Danh sách sản phẩm ưu đãi đặc biệt (Special Offers)
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public async Task<List<ProductViewModel>> GetSpecialOfferProductAsync(int top)
        {
            return await _productService.GetAll(x => x.Status == Status.Active && x.AlwaysOnTheHomePage == true)
            .OrderByDescending(x => x.DateCreated)
            .Take(top)
            .ProjectTo<ProductViewModel>()
            .ToListAsync();
        }

        public async Task<List<TagViewModel>> GetProductTagAsync(long productId)
        {
            var tags = _tagService.GetAll();
            var productTags = _productTagService.GetAll();

            var query = from t in tags
                        join pt in productTags
                        on t.Id equals pt.TagId
                        where pt.ProductId == productId
                        select new TagViewModel()
                        {
                            Id = t.Id,
                            Name = t.Name
                        };
            return await query.ToListAsync();
        }

        public async Task<List<ProductImageViewModel>> GetImagesAsync(long productId)
        {
            return await _productImageService.GetAll(x => x.ProductId == productId)
                .ProjectTo<ProductImageViewModel>().ToListAsync();
        }

        public async Task<List<ProductImage>> AddImagesAsync(long productId, string[] images)
        {
            await _productImageService.DeleteRangeAsync(_productImageService.GetAll(x => x.ProductId == productId).ToList());

            List<ProductImage> imgEntities = new List<ProductImage>();
            foreach (var image in images)
            {
                imgEntities.Add(new ProductImage()                
                {
                    Path = image,
                    ProductId = productId,
                    Caption = string.Empty
                });
            }

            var result = await _productImageService.InsertRangeAsync(imgEntities);
            return result;
        }

        public async Task ImportExcelAsync(string filePath, int categoryId)
        {
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                Product product;
                for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                {
                    product = new Product();
                    product.CategoryId = categoryId;
                    product.Name = workSheet.Cells[i, 1].Value.ToString();
                    product.Description = workSheet.Cells[i, 2].Value.ToString();
                    decimal.TryParse(workSheet.Cells[i, 3].Value.ToString(), out var originalPrice);
                    product.OriginalPrice = originalPrice;
                    decimal.TryParse(workSheet.Cells[i, 4].Value.ToString(), out var price);
                    product.Price = price;
                    decimal.TryParse(workSheet.Cells[i, 5].Value.ToString(), out var promotionPrice);
                    product.PromotionPrice = promotionPrice;
                    product.Content = workSheet.Cells[i, 6].Value.ToString();
                    product.MetaKeywords = workSheet.Cells[i, 7].Value.ToString();

                    product.MetaDescription = workSheet.Cells[i, 8].Value.ToString();
                    bool.TryParse(workSheet.Cells[i, 9].Value.ToString(), out var hotFlag);

                    product.HotFlag = hotFlag;
                    bool.TryParse(workSheet.Cells[i, 10].Value.ToString(), out var homeFlag);
                    product.HomeFlag = homeFlag;

                    product.Status = Status.Active;

                    await _productService.InsertAsync(product);
                }
            }
        }
    }
}
