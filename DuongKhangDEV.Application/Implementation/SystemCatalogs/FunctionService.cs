using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DuongKhangDEV.Application.Interfaces;
using DuongKhangDEV.Application.ViewModels.SystemCatalog;
using DuongKhangDEV.Data.Entities;
using DuongKhangDEV.Data.Enums;
using DuongKhangDEV.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using DuongKhangDEV.Utilities.Dtos;
using DuongKhangDEV.Application.Interfaces.SystemCatalog;
using DuongKhangDEV.Data.Entities.SystemCatalog;

namespace DuongKhangDEV.Application.Implementation.SystemCatalog
{
    public class FunctionService : WebServiceBase<Function, string, FunctionViewModel>, IFunctionService
    {
        private IRepository<Function, string> _functionService;
        private IRepository<Permission, int> _permissionService;
        private RoleManager<AppRole> _roleManager;
        private UserManager<AppUser> _userManager;
        private IUnitOfWork _unitOfWork;

        public FunctionService(
            IRepository<Function, string> functionService,
             RoleManager<AppRole> roleManager,
              UserManager<AppUser> userManager,
             IRepository<Permission, int> permissionService,
            IUnitOfWork unitOfWork) : base(functionService, unitOfWork)
        {
            _functionService = functionService;
            _userManager = userManager;
            _roleManager = roleManager;
            _permissionService = permissionService;
            _unitOfWork = unitOfWork;
        }

        public bool CheckExistedId(string id)
        {
            return _functionService.GetById(id) != null;
        }

        public async Task<List<FunctionViewModel>> GetAllFilterAsync(string filter)
        {
            var query = _functionService.GetAll(x => x.Status == Status.Active);
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.Name.Contains(filter));
            }

            return await query.OrderBy(x => x.ParentId).ProjectTo<FunctionViewModel>().ToListAsync();
        }

        public IEnumerable<FunctionViewModel> GetAllWithParentId(string parentId)
        {
            return _functionService.GetAll(x => x.ParentId == parentId).ProjectTo<FunctionViewModel>();
        }

        public async Task<List<FunctionViewModel>> GetAllWithPermission(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var roles = await _userManager.GetRolesAsync(user);

            var query = (from f in _functionService.GetAll()
                         join p in _permissionService.GetAll() on f.Id equals p.FunctionId
                         join r in _roleManager.Roles on p.RoleId equals r.Id
                         where roles.Contains(r.Name)
                         select f);

            var parentIds = query.Select(x => x.ParentId).Distinct();

            query = query.Union(_functionService.GetAll().Where(f => parentIds.Contains(f.Id)));

            return await query.OrderBy(x => x.ParentId).ProjectTo<FunctionViewModel>().ToListAsync();
        }

        public void ReOrder(string sourceId, string targetId)
        {
            var source = _functionService.GetById(sourceId);
            var target = _functionService.GetById(targetId);
            int tempOrder = source.SortOrder;

            source.SortOrder = target.SortOrder;
            target.SortOrder = tempOrder;

            _functionService.Update(source);
            _functionService.Update(target);
        }

        public void UpdateParentId(string sourceId, string targetId, Dictionary<string, string> items)
        {
            //Update parent id for source
            var category = _functionService.GetById(sourceId);
            category.ParentId = targetId;
            _functionService.Update(category);

            //Get all sibling
            var sibling = _functionService.GetAll(x => items.ContainsKey(x.Id.ToString()));
            foreach (var child in sibling)
            {
                //child.SortOrder = items[child.Id.ToString()];
                _functionService.Update(child);
            }
        }
    }
}