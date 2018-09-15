using DuongKhangDEV.Data.EF.Configurations;
using DuongKhangDEV.Data.EF.Extensions;
using DuongKhangDEV.Data.Entities;
using DuongKhangDEV.Data.Entities.Advs;
using DuongKhangDEV.Data.Entities.BlogCatalog;
using DuongKhangDEV.Data.Entities.Commons;
using DuongKhangDEV.Data.Entities.Content;
using DuongKhangDEV.Data.Entities.ECommerce;
using DuongKhangDEV.Data.Entities.ProductCatalog;
using DuongKhangDEV.Data.Entities.SystemCatalog;
using DuongKhangDEV.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DuongKhangDEV.Data.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<AppRole> AppRoles { get; set; }

        public DbSet<Function> Functions { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<SystemConfig> SystemConfigs { get; set; }
        
        public DbSet<Bill> Bills { set; get; }
        public DbSet<BillDetail> BillDetails { set; get; }
        public DbSet<Blog> Blogs { set; get; }
        public DbSet<BlogTag> BlogTags { set; get; }
        
        public DbSet<Contact> Contacts { set; get; }
        public DbSet<Feedback> Feedbacks { set; get; }
        public DbSet<Footer> Footers { set; get; }
        public DbSet<Page> Pages { set; get; }
        public DbSet<Product> Products { set; get; }
        public DbSet<ProductCategory> ProductCategories { set; get; }
        public DbSet<ProductImage> ProductImages { set; get; }
        public DbSet<ProductQuantity> ProductQuantities { set; get; }
        public DbSet<ProductTag> ProductTags { set; get; }
        public DbSet<Slide> Slides { set; get; }

        public DbSet<Tag> Tags { set; get; }
        
        public DbSet<WholePrice> WholePrices { get; set; }

        public DbSet<AdvertistmentPage> AdvertistmentPages { get; set; }
        public DbSet<Advertistment> Advertistments { get; set; }
        public DbSet<AdvertistmentPosition> AdvertistmentPositions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Identity Config

            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims").HasKey(x => x.Id);

            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims")
                .HasKey(x => x.Id);

            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);

            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles")
                .HasKey(x => new { x.RoleId, x.UserId });

            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens")
               .HasKey(x => new { x.UserId });

            #endregion Identity Config

            modelBuilder.AddConfiguration(new TagConfiguration());
            modelBuilder.AddConfiguration(new BlogTagConfiguration());
            //modelBuilder.AddConfiguration(new ContactDetailConfiguration());

            modelBuilder.AddConfiguration(new FunctionConfiguration());
            //modelBuilder.AddConfiguration(new PageConfiguration());
            modelBuilder.AddConfiguration(new FooterConfiguration());

            modelBuilder.AddConfiguration(new ProductTagConfiguration());
            modelBuilder.AddConfiguration(new SystemConfigConfiguration());
            modelBuilder.AddConfiguration(new AdvertistmentPositionConfiguration());

        }

        public override int SaveChanges()
        {
            TrackChanges();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            TrackChanges();
            return await base.SaveChangesAsync(cancellationToken);
        }

        //public override int SaveChanges()
        //{
        //    //try
        //    {
        //        var modified = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);
        //        foreach (EntityEntry item in modified)
        //        {
        //            if (item.Entity is IDateTracking changedOrAddedItem)
        //            {
        //                if (item.State == EntityState.Added)
        //                {
        //                    changedOrAddedItem.DateCreated = DateTime.Now;
        //                }
        //                changedOrAddedItem.DateModified = DateTime.Now;
        //            }
        //        }
        //        return base.SaveChanges();
        //    }
        //    //catch ()
        //    {
        //        //throw new ModelValidationException(entityException.Message);
        //    }
        //}

        //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        //{
        //    var modified = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);
        //    foreach (EntityEntry item in modified)
        //    {
        //        if (item.Entity is IDateTracking changedOrAddedItem)
        //        {
        //            if (item.State == EntityState.Added)
        //            {
        //                changedOrAddedItem.DateCreated = DateTime.Now;
        //            }
        //            changedOrAddedItem.DateModified = DateTime.Now;
        //        }
        //    }
        //    return await base.SaveChangesAsync(cancellationToken);
        //}

        public string UserProvider
        {
            get
            {
                if (!string.IsNullOrEmpty(WindowsIdentity.GetCurrent().Name))
                    return WindowsIdentity.GetCurrent().Name.Split('\\')[1];
                return string.Empty;
            }
        }

        private void TrackChanges()
        {
            foreach (var entry in this.ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                if (entry.Entity is IDateTracking)
                {
                    var auditable = entry.Entity as IDateTracking;
                    if (entry.State == EntityState.Added)
                    {
                        //auditable.CreatedBy = UserProvider;//  
                        auditable.DateCreated = DateTime.Now;
                        //auditable.UpdatedOn = TimestampProvider();
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        //auditable.UpdatedBy = UserProvider;
                        auditable.DateModified = DateTime.Now;
                    }
                }
            }
        }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = configuration.GetConnectionString("AppDbConnection");
            builder.UseSqlServer(connectionString);
            return new AppDbContext(builder.Options);
        }
    }
}
