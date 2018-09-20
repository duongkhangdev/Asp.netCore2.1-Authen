using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DuongKhangDEV.Application.ViewModels.SystemCatalog;
using DuongKhangDEV.Utilities.Dtos;

namespace DuongKhangDEV.Application.Interfaces.SystemCatalog
{
    public interface IRoleService : IDisposable
    {
        Task<bool> AddAsync(AppRoleViewModel userVm);

        Task DeleteAsync(Guid id);

        Task<List<AppRoleViewModel>> GetAllAsync();

        Task<PagedResult<AppRoleViewModel>> GetAllPagingAsync(string keyword, int page, int pageSize);

        Task<AppRoleViewModel> GetByIdAsync(Guid id);


        Task UpdateAsync(AppRoleViewModel userVm);

        Task<List<PermissionViewModel>> GetListFunctionWithRoleAsync(Guid roleId);

        void SavePermission(List<PermissionViewModel> permissions, Guid roleId);

        Task<bool> CheckPermissionAsync(string functionId, string action, string[] roles);
    }
}
