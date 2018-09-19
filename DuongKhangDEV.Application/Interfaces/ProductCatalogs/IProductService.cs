using DuongKhangDEV.Application.ViewModels.Common;
using DuongKhangDEV.Application.ViewModels.ProductCatalog;
using DuongKhangDEV.Data.Entities.ProductCatalog;
using DuongKhangDEV.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DuongKhangDEV.Application.Interfaces.ProductCatalogs
{
    public interface IProductService : IWebServiceBase<Product, long, ProductViewModel>
    {


        Task<List<ProductViewModel>> GetLastestAsync(int top);

        Task<List<ProductViewModel>> GetHotProductAsync(int top);

        Task<List<ProductViewModel>> GetRelatedProductAsync(long id, int top);

        Task<List<ProductViewModel>> GetUpsellProductAsync(int top);

        Task<List<ProductViewModel>> GetUpsellProductAsync(int top, bool showOnMainMenu);

        Task<List<ProductViewModel>> GetPopularProductAsync(int top);

        Task<List<ProductViewModel>> GetSpecialOfferProductAsync(int top);

        Task<List<TagViewModel>> GetProductTagAsync(long productId);

        Task<List<ProductImageViewModel>> GetImagesAsync(long productId);

        Task<List<ProductImage>> AddImagesAsync(long productId, string[] images);

        Task ImportExcelAsync(string filePath, int categoryId);
    }
}
