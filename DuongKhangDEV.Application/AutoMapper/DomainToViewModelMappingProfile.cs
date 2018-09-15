using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using DuongKhangDEV.Application.ViewModels.Blog;
using DuongKhangDEV.Application.ViewModels.Common;
using DuongKhangDEV.Application.ViewModels.ProductCatalog;
using DuongKhangDEV.Application.ViewModels.SystemCatalog;
using DuongKhangDEV.Data.Entities;
using DuongKhangDEV.Application.ViewModels;
using DuongKhangDEV.Data.Entities.Content;
using DuongKhangDEV.Application.ViewModels.Content;
using DuongKhangDEV.Data.Entities.Advs;
using DuongKhangDEV.Application.ViewModels.Advs;
using DuongKhangDEV.Data.Entities.BlogCatalog;
using DuongKhangDEV.Data.Entities.ECommerce;
using DuongKhangDEV.Application.ViewModels.ECommerce;
using DuongKhangDEV.Data.Entities.ProductCatalog;
using DuongKhangDEV.Application.ViewModels.BlogCatalog;
using DuongKhangDEV.Data.Entities.SystemCatalog;

namespace DuongKhangDEV.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            #region Common



            #endregion

            #region ECommerce
            
            CreateMap<Bill, BillViewModel>();
            CreateMap<BillDetail, BillDetailViewModel>();            

            #endregion

            #region Product Catalog

            CreateMap<ProductCategory, ProductCategoryViewModel>();
            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductQuantity, ProductQuantityViewModel>().MaxDepth(2);
            CreateMap<ProductImage, ProductImageViewModel>().MaxDepth(2);
            CreateMap<ProductReview, ProductReviewViewModel>().MaxDepth(2);
            CreateMap<WholePrice, WholePriceViewModel>().MaxDepth(2);
            CreateMap<ProductWishlist, ProductWishlistViewModel>().MaxDepth(2);
            CreateMap<ProductComparison, ProductComparisonViewModel>().MaxDepth(2);

            #endregion

            #region System

            CreateMap<Function, FunctionViewModel>();
            CreateMap<AppUser, AppUserViewModel>();
            CreateMap<AppRole, AppRoleViewModel>();
            CreateMap<Newsletter, NewsletterViewModel>();
            CreateMap<VisitorStatistic, VisitorStatisticViewModel>();
            CreateMap<SystemConfig, SystemConfigViewModel>().MaxDepth(2);
            CreateMap<Menu, MenuViewModel>().MaxDepth(2);
            CreateMap<MenuGroup, MenuGroupViewModel>().MaxDepth(2);

            #endregion

            #region Content
            
            CreateMap<Feedback, FeedbackViewModel>().MaxDepth(2);
            CreateMap<Contact, ContactViewModel>().MaxDepth(2);
            CreateMap<Page, PageViewModel>().MaxDepth(2);
            CreateMap<Footer, FooterViewModel>().MaxDepth(2);

            #endregion

            #region Blog Catalog

            CreateMap<BlogInCategory, BlogInCategoryViewModel>().MaxDepth(2);
            CreateMap<BlogCategory, BlogCategoryViewModel>().MaxDepth(2);
            CreateMap<Blog, BlogViewModel>().MaxDepth(2);
            CreateMap<BlogTag, BlogTagViewModel>().MaxDepth(2);
            CreateMap<BlogComment, BlogCommentViewModel>().MaxDepth(2);
            CreateMap<BlogCommentAnswer, BlogCommentAnswerViewModel>().MaxDepth(2);

            #endregion

            #region Advs

            CreateMap<BannerGroup, BannerGroupViewModel>().MaxDepth(2);
            CreateMap<Banner, BannerViewModel>().MaxDepth(2);
            CreateMap<SlideGroup, SlideGroupViewModel>().MaxDepth(2);
            CreateMap<Slide, SlideViewModel>().MaxDepth(2);

            #endregion
        }
    }
}
