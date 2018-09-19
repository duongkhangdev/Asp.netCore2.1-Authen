using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DuongKhangDEV.Data.EF;
using DuongKhangDEV.Data.Entities.SystemCatalog;
using Newtonsoft.Json.Serialization;
using DuongKhangDEV.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using DuongKhangDEV.WebApp.Services;
using DuongKhangDEV.WebApp.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.FileProviders;
using System.IO;
using DuongKhangDEV.WebApp.Helpers;
using DuongKhangDEV.Application.Interfaces.SystemCatalog;
using DuongKhangDEV.Application.Implementation.SystemCatalog;
using DuongKhangDEV.Application.Interfaces.ProductCatalogs;
using DuongKhangDEV.Application.Implementation.ProductCatalogs;

namespace DuongKhangDEV.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AppDbConnection"),
                o => o.MigrationsAssembly("DuongKhangDEV.Data.EF")));

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            #region Configure Identity

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true; // có yêu cầu ký tự là chữ số
                options.Password.RequiredLength = 6; // độ dài 6 ký tự
                options.Password.RequireNonAlphanumeric = false; // không yêu cầu ký tự đặc biệt
                options.Password.RequireUppercase = false; // không yêu cầu chữ HOA
                options.Password.RequireLowercase = false; // không yêu cầu chữ thường

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            #endregion

            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
            });

            #region AddSession - Giỏ hàng

            services.AddSession(options =>
            {
                //options.IdleTimeout = TimeSpan.FromHours(2); // Timeout trong vòng 2h
                options.Cookie.HttpOnly = true;                     // Cài đặt cho Cookies, chỉ cho Http
            });

            #endregion

            services.AddAutoMapper();  //AutoMapper.Extensions.Microsoft.DependencyInjection

            //Register for DI
            services.AddScoped<SignInManager<AppUser>, SignInManager<AppUser>>();
            services.AddScoped<UserManager<AppUser>, UserManager<AppUser>>();
            services.AddScoped<RoleManager<AppRole>, RoleManager<AppRole>>();

            #region Auto Mapper
            
            services.AddSingleton(Mapper.Configuration);
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService));

            #endregion

            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IViewRenderService, ViewRenderService>();

            #region Systems

            services.AddTransient<DbInitializer>();

            services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, CustomClaimsPrincipalFactory>(); // Hiển thị thông tin đăng nhập qua Claim

            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.AddTransient(typeof(IUnitOfWork), typeof(EFUnitOfWork));
            services.AddTransient(typeof(IRepository<,>), typeof(EFRepository<,>));

            #endregion

            #region Services

            #region Common

            //services.AddTransient<ICommonService, CommonService>();
            //services.AddTransient<IBankAccountService, BankAccountService>();
            //services.AddTransient<ISupportOnlineService, SupportOnlineService>();
            //services.AddTransient<ICustomerBoxService, CustomerBoxService>();

            #endregion

            #region ECommerce

            //services.AddTransient<IBillService, BillService>();

            #endregion

            #region Product Catalog

            services.AddTransient<IProductCategoryService, ProductCategoryService>();
            services.AddTransient<IProductService, ProductService>();
            //services.AddTransient<IVendorService, VendorService>();
            //services.AddTransient<IManufacturerService, ManufacturerService>();

            #endregion

            #region System

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IFunctionService, FunctionService>();
            //services.AddTransient<INewsletterService, NewsletterService>();
            //services.AddTransient<IMenuGroupService, MenuGroupService>();
            //services.AddTransient<IMenuService, MenuService>();

            #endregion

            #region Content

            //services.AddTransient<IPageService, PageService>();
            //services.AddTransient<IFeedbackService, FeedbackService>();
            //services.AddTransient<IContactService, ContactService>();
            //services.AddTransient<IFooterService, FooterService>();

            #endregion
            //services.AddTransient<IFaqService, FaqService>();

            #region Blog Catalog

            //services.AddTransient<IBlogInCategoryService, BlogInCategoryService>();
            //services.AddTransient<IBlogCategoryService, BlogCategoryService>();
            //services.AddTransient<IBlogService, BlogService>();
            //services.AddTransient<IBlogCommentService, BlogCommentService>();
            //services.AddTransient<IBlogCommentAnswerService, BlogCommentAnswerService>();

            #endregion

            #region Advs

            //services.AddTransient<IBannerService, BannerService>();
            //services.AddTransient<IBannerGroupService, BannerGroupService>();
            //services.AddTransient<ISlideService, SlideService>();
            //services.AddTransient<ISlideGroupService, SlideGroupService>();

            #endregion
            //services.AddTransient<IReportService, ReportService>();

            #endregion

            // Áp dụng quyền trên giao diện người dùng
            services.AddTransient<IAuthorizationHandler, BaseResourceAuthorizationHandler>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/duongkhang-{Date}.txt"); // Serilog.Extensions.Logging.File

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            // For wwwroot directory
            app.UseStaticFiles();

            // Add support for node_modules but only during development **temporary**
            if (env.IsDevelopment())
            {
                app.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(
                      Path.Combine(Directory.GetCurrentDirectory(), @"node_modules")),
                    RequestPath = new PathString("/vendor")
                });
            }

            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                // Admin
                routes.MapRoute(name: "areaRoute",
                    template: "{area:exists}/{controller=Login}/{action=Index}/{id?}");

                //routes.MapRoute(name: "areaRoute",
                //    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
