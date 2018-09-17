using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DuongKhangDEV.Application.Interfaces;
using DuongKhangDEV.Application.ViewModels.SystemCatalog;
using DuongKhangDEV.WebApp.Extensions;
using DuongKhangDEV.Utilities.Constants;
using DuongKhangDEV.Application.Interfaces.SystemCatalog;

namespace DuongKhangDEV.WebApp.Areas.Admin.Components
{
    public class SideBarViewComponent : ViewComponent
    {
        private IFunctionService _functionService;

        public SideBarViewComponent(IFunctionService functionService)
        {
            _functionService = functionService;
        }

        //public async Task<IViewComponentResult> InvokeAsync()
        //{
        //    var roles = ((ClaimsPrincipal)User).GetSpecificClaim("Roles");

        //    List<FunctionViewModel> functions;

        //    if (roles.Split(";").Contains(CommonConstants.AppRole.AdminRole))
        //    {
        //        functions = await _functionService.GetAll(string.Empty);
        //    }
        //    else
        //    {
        //        //TODO: Get by permission
        //        functions = await _functionService.GetAll(string.Empty);
        //    }
        //    return View(functions);
        //}

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var roles = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == CommonConstants.UserClaims.Roles);

            List<FunctionViewModel> functions;

            if (roles != null && roles.Value.Split(";").Contains(CommonConstants.AppRole.AdminRole))
            {
                functions = await _functionService.GetAll(string.Empty);
            }
            else
            {
                //  Get by permission
                functions = await _functionService.GetAllWithPermission(User.Identity.Name);
                //functions = await _functionService.GetAll(string.Empty);
            }
            return View(functions);
        }
    }
}