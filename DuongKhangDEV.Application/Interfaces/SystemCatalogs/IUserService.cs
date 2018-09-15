using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DuongKhangDEV.Application.ViewModels.SystemCatalog;
using DuongKhangDEV.Utilities.Dtos;

namespace DuongKhangDEV.Application.Interfaces.SystemCatalog
{
    public interface IUserService : IDisposable
    {
        Task<bool> AddAsync(AppUserViewModel userVm);

        Task DeleteAsync(string id);

        Task<List<AppUserViewModel>> GetAllAsync();

        PagedResult<AppUserViewModel> GetAllPagingAsync(string keyword, int page, int pageSize);

        Task<AppUserViewModel> GetById(string id);


        Task UpdateAsync(AppUserViewModel userVm);
    }
}
