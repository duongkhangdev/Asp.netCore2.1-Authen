using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DuongKhangDEV.Application.ViewModels.SystemCatalog;
using DuongKhangDEV.Utilities.Dtos;

namespace DuongKhangDEV.Application.Interfaces.SystemCatalog
{
    public interface IFunctionService : IDisposable
    {
        void Add(FunctionViewModel function);

        Task<List<FunctionViewModel>> GetAll(string filter);

        IEnumerable<FunctionViewModel> GetAllWithParentId(string parentId);

        Task<List<FunctionViewModel>> GetAllWithPermission(string userName);

        FunctionViewModel GetById(string id);

        PagedResult<FunctionViewModel> GetAllPaging(string keyword, int page, int pageSize);

        void Update(FunctionViewModel function);

        void Delete(string id);

        void Save();

        bool CheckExistedId(string id);

        void UpdateParentId(string sourceId, string targetId, Dictionary<string, string> items);

        void ReOrder(string sourceId, string targetId);
    }
}