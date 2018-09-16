using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DuongKhangDEV.Data.Entities;
using DuongKhangDEV.Data.Entities.SystemCatalog;

namespace DuongKhangDEV.WebApp.Helpers
{
    public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser, AppRole>
    {
        private readonly UserManager<AppUser> _userManager;
        //UserManager<AppUser> _userManager;

        public CustomClaimsPrincipalFactory(UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager, 
            IOptions<IdentityOptions> options)
            : base(userManager, roleManager, options)
        {
            _userManager = userManager;
        }

        public async override Task<ClaimsPrincipal> CreateAsync(AppUser user)
        {
            var principal = await base.CreateAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            // Add your claims here
            ((ClaimsIdentity)principal.Identity).AddClaims(new[]
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim("UserName", user.UserName),
                new Claim("Email",user.Email),
                //new Claim(ClaimTypes.Email, user.Email),
                new Claim("FullName", user.FullName),
                //new Claim("Avatar", user.Avatar??string.Empty),
                new Claim("Avatar", string.IsNullOrEmpty(user.Avatar) ? string.Empty : user.Avatar),
                new Claim("Roles", string.Join(";",roles))
            });

            return principal;
        }
    }
}
