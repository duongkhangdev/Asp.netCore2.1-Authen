using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DuongKhangDEV.Application.ViewModels.SystemCatalog;
using DuongKhangDEV.Data.Entities.SystemCatalog;
using DuongKhangDEV.Utilities.Dtos;

namespace DuongKhangDEV.Application.Interfaces.SystemCatalog
{
    public interface IFunctionService : IWebServiceBase<Function, string, FunctionViewModel>
    {
        Task<List<FunctionViewModel>> GetAllFilterAsync(string filter);

        IEnumerable<FunctionViewModel> GetAllWithParentId(string parentId);

        Task<List<FunctionViewModel>> GetAllWithPermission(string userName);

        bool CheckExistedId(string id);

        void UpdateParentId(string sourceId, string targetId, Dictionary<string, string> items);

        void ReOrder(string sourceId, string targetId);
    }
}