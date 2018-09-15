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
using DuongKhangDEV.Data.Entities.Commons;
using DuongKhangDEV.Data.Entities.Content;
using DuongKhangDEV.Application.ViewModels.Content;
using DuongKhangDEV.Application.ViewModels.Advs;
using DuongKhangDEV.Data.Entities.Advs;
using DuongKhangDEV.Data.Entities.BlogCatalog;
using DuongKhangDEV.Data.Entities.ProductCatalog;
using DuongKhangDEV.Application.ViewModels.ECommerce;
using DuongKhangDEV.Data.Entities.ECommerce;
using DuongKhangDEV.Application.ViewModels.BlogCatalog;
using DuongKhangDEV.Data.Entities.SystemCatalog;

namespace DuongKhangDEV.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            #region ECommerce

            CreateMap<BillViewModel, Bill>()
              .ConstructUsing(c => new Bill(c.Id, c.CustomerName, c.CustomerAddress,
              c.CustomerMobile, c.CustomerEmail, c.CustomerZalo, c.CustomerFacebook, c.CustomerMessage, 
              c.BillStatus, c.PaymentMethod, c.Status, c.CustomerId));

            CreateMap<BillDetailViewModel, BillDetail>()
              .ConstructUsing(c => new BillDetail(c.Id, c.BillId, c.ProductId,
              c.Quantity, c.Price));

            #endregion

            #region Product Catalog

            CreateMap<ProductCategoryViewModel, ProductCategory>()
                .ConstructUsing(c => new ProductCategory(c.Id, c.Name, c.Description, c.ParentId, c.HomeOrder, c.ThumbnailImage, c.UniqueCode, c.HomeFlag,
                c.SortOrder, c.Status, c.MetaTitle, c.MetaAlias, c.MetaKeywords, c.MetaDescription));

            CreateMap<ProductViewModel, Product>()
           .ConstructUsing(c => new Product(c.Id, c.Name, c.CategoryId, c.ThumbnailImage, c.Price, c.OriginalPrice,
           c.PromotionPrice, c.Description, c.Content,
           c.HomeFlag, c.HotFlag, c.HotSalesFlag, c.HotNewFlag, c.AlwaysOnTheHomePage, c.AlwaysOnTheMainMenu, c.AllowAddedToCart,
           c.Tags, c.Unit, c.Status,
           c.MetaTitle, c.MetaAlias, c.MetaKeywords, c.MetaDescription));

            CreateMap<ProductWishlistViewModel, ProductWishlist>()
                .ConstructUsing(c => new ProductWishlist(c.Id, c.UserId, c.ProductId));

            CreateMap<ProductComparisonViewModel, ProductComparison>()
                .ConstructUsing(c => new ProductComparison(c.Id, c.UserId, c.ProductId));

            #endregion

            #region System

            CreateMap<AppUserViewModel, AppUser>()
            .ConstructUsing(c => new AppUser(c.Id.GetValueOrDefault(Guid.Empty), c.FullName, c.UserName, 
            c.Email, c.PhoneNumber, c.Address, c.City, c.Avatar, c.Status));

            CreateMap<PermissionViewModel, Permission>()
            .ConstructUsing(c => new Permission(c.RoleId, c.FunctionId, c.CanCreate, c.CanRead, c.CanUpdate, c.CanDelete));

            CreateMap<FunctionViewModel, Function>()
            .ConstructUsing(c => new Function(c.Id, c.Name, c.URL, c.ParentId, c.IconCss, c.SortOrder, c.Status));

            CreateMap<NewsletterViewModel, Newsletter>()
            .ConstructUsing(c => new Newsletter(c.Id, c.Email, c.Status));

            CreateMap<VisitorStatisticViewModel, VisitorStatistic>()
            .ConstructUsing(c => new VisitorStatistic(c.Id, c.VisitedDate, c.IPAddress));

            CreateMap<MenuGroupViewModel, MenuGroup>()
              .ConstructUsing(c => new MenuGroup(c.Id, c.Name, c.Status));

            CreateMap<MenuViewModel, Menu>()
            .ConstructUsing(c => new Menu(c.Id, c.Name, c.ParentId, c.HomeOrder, c.Url, c.Target, c.ThumbnailImage, c.Description, c.UniqueCode, c.DisplayOrder, c.Status, c.GroupId));

            #endregion

            #region Content

            CreateMap<ContactViewModel, Contact>()
                .ConstructUsing(c => new Contact(c.Id, c.Name, c.Phone, c.Email, c.Zalo, c.Facebook, c.Skype, c.Website, c.Address, c.Other, c.Lng, c.Lat, c.Status));

            CreateMap<FeedbackViewModel, Feedback>()
                .ConstructUsing(c => new Feedback(c.Id, c.Name, c.Email, c.Phone, c.Message, c.Status));

            CreateMap<PageViewModel, Page>()
             .ConstructUsing(c => new Page(c.Id, c.Name, c.Alias, c.Target, c.Content, c.Status, c.MetaTitle, c.MetaAlias, c.MetaKeywords, c.MetaDescription));            

            CreateMap<FooterViewModel, Footer>()
              .ConstructUsing(c => new Footer(c.Id, c.Content, c.Status));

            #endregion

            #region Blog Catalog

            CreateMap<BlogViewModel, Blog>()
              .ConstructUsing(c => new Blog(c.Name, c.ThumbnailImage, c.Description,
              c.Content, c.HomeFlag, c.HotFlag, c.Tags, c.Status,
              c.MetaTitle, c.MetaAlias, c.MetaKeywords, c.MetaDescription));

            CreateMap<BlogCategoryViewModel, BlogCategory>()
              .ConstructUsing(c => new BlogCategory(c.Name, c.Description, c.ParentId, c.HomeOrder, c.ThumbnailImage, c.HomeFlag,
                c.SortOrder, c.Status, c.MetaTitle, c.MetaAlias, c.MetaKeywords, c.MetaDescription));

            CreateMap<BlogInCategoryViewModel, BlogInCategory>()
             .ConstructUsing(c => new BlogInCategory(c.Id, c.BlogId, c.BlogCategoryId));

            CreateMap<BlogViewModel, Blog>()
              .ConstructUsing(c => new Blog(c.Name, c.ThumbnailImage, c.Description,
              c.Content, c.HomeFlag, c.HotFlag, c.Tags, c.Status,
              c.MetaTitle, c.MetaAlias, c.MetaKeywords, c.MetaDescription));

            CreateMap<BlogTagViewModel, BlogTag>()
              .ConstructUsing(c => new BlogTag(c.Id, c.BlogId, c.TagId));

            CreateMap<BlogCommentViewModel, BlogComment>()
              .ConstructUsing(c => new BlogComment(c.Id, c.BlogId, c.Name, c.Email, c.Content, c.Status, c.CustomerId));

            CreateMap<BlogCommentAnswerViewModel, BlogCommentAnswer>()
              .ConstructUsing(c => new BlogCommentAnswer(c.Id, c.BlogCommentId, c.Content, c.Status, c.AnswerId));

            #endregion

            #region Advs

            CreateMap<BannerGroupViewModel, BannerGroup>()
              .ConstructUsing(c => new BannerGroup(c.Id, c.Name, c.Status));

            CreateMap<BannerViewModel, Banner>()
              .ConstructUsing(c => new Banner(c.Id, c.Name, c.Title, c.PositionDescription, c.Description, c.ThumbnailImage, c.Url, c.GroupId, c.DisplayOrder, c.BannerOrder, c.Status, c.Content, c.GroupAlias, c.ButtonText, c.Target));

            CreateMap<SlideGroupViewModel, SlideGroup>()
              .ConstructUsing(c => new SlideGroup(c.Id, c.Name, c.Status));

            CreateMap<SlideViewModel, Slide>()
              .ConstructUsing(c => new Slide(c.Id, c.Name, c.Title, c.Description, c.ThumbnailImage, c.Url, c.GroupId, c.DisplayOrder, c.SlideOrder, c.Status, c.Content1, c.Content2, c.Content3, c.Content4, c.Content5, c.GroupAlias, c.Target, c.ButtonText));

            #endregion

            #region Common

            

            #endregion
        }
    }
}
