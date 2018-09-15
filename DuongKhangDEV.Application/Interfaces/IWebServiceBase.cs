using DuongKhangDEV.Data.Enums;
using DuongKhangDEV.Infrastructure.SharedKernel;
using DuongKhangDEV.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DuongKhangDEV.Application.Interfaces
{
    public interface IWebServiceBase<TEntity, TPrimaryKey, ViewModel> where ViewModel : class
        where TEntity : DomainEntity<TPrimaryKey>
    {
        #region Not Async

        void Add(ViewModel viewModel);

        ViewModel Insert(ViewModel entity);

        TPrimaryKey InsertAndGetId(ViewModel entity);

        void Update(ViewModel viewModel);

        void Update(ViewModel viewModel, object key);

        void Delete(TPrimaryKey id);

        void Delete(ViewModel entity);

        void DeleteMultiple(IEnumerable<ViewModel> entities);

        void DeleteMultiple(List<ViewModel> entities);

        ViewModel GetById(TPrimaryKey id);

        List<ViewModel> GetAll();

        PagedResult<ViewModel> GetAllPaging(Expression<Func<TEntity, bool>> predicate, Func<TEntity, object> orderBy,
            SortDirection sortDirection, int pageIndex, int pageSize);

        PagedResult<ViewModel> GetAllPaging(Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize);

        PagedResult<ViewModel> GetAllPaging(int pageIndex, int pageSize);

        List<ViewModel> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors);

        List<ViewModel> GetAllIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] propertySelectors);

        bool CheckExits(object key);

        bool Exists(Expression<Func<TEntity, bool>> predicate);

        #endregion

        #region Async

        Task<int> CountAsync();

        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

        Task<long> LongCountAsync();

        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate);

        Task<List<ViewModel>> GetAllAsync();

        Task<List<ViewModel>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);

        Task<List<ViewModel>> GetAllIncludingAsync(params Expression<Func<TEntity, object>>[] propertySelectors);

        Task<List<ViewModel>> GetAllIncludingAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] propertySelectors);

        Task<ViewModel> GetByIdAsync(TPrimaryKey id);

        Task<ViewModel> GetByAsync(ViewModel entity);

        Task<ViewModel> InsertAsync(ViewModel entity);

        Task<TPrimaryKey> InsertAndGetIdAsync(ViewModel entity);

        Task<ViewModel> UpdateAsync(ViewModel entity);

        Task<ViewModel> UpdateAsync(ViewModel entity, object key);

        Task<ViewModel> DeleteAsync(ViewModel entity);

        Task<int> DeleteAsync(TPrimaryKey id);

        Task<IEnumerable<ViewModel>> DeleteRangeAsync(IEnumerable<ViewModel> entities);

        Task<List<ViewModel>> DeleteRangeAsync(List<ViewModel> entities);

        Task<bool> CheckExitsAsync(object key);

        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);

        Task<PagedResult<ViewModel>> GetAllPagingAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> orderBy,
            SortDirection sortDirection, int pageIndex, int pageSize);

        Task<PagedResult<ViewModel>> GetAllPagingAsync(Expression<Func<TEntity, bool>> predicate,int pageIndex, int pageSize);

        Task<PagedResult<ViewModel>> GetAllPagingAsync(int pageIndex, int pageSize);

        #endregion
    }
}
