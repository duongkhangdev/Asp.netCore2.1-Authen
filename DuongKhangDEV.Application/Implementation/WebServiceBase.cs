using AutoMapper;
using AutoMapper.QueryableExtensions;
using DuongKhangDEV.Application.Interfaces;
using DuongKhangDEV.Data.Enums;
using DuongKhangDEV.Infrastructure.Interfaces;
using DuongKhangDEV.Infrastructure.SharedKernel;
using DuongKhangDEV.Utilities.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DuongKhangDEV.Application.Implementation
{
    public abstract class WebServiceBase<TEntity, TPrimaryKey, ViewModel> : IWebServiceBase<TEntity, TPrimaryKey, ViewModel>, IDisposable
        where ViewModel : class
        where TEntity : DomainEntity<TPrimaryKey>
    {
        private readonly IRepository<TEntity, TPrimaryKey> _repository;
        private readonly IUnitOfWork _unitOfWork;

        protected WebServiceBase(IRepository<TEntity, TPrimaryKey> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        #region Not Async

        public virtual void Add(ViewModel viewModel)
        {
            var model = Mapper.Map<ViewModel, TEntity>(viewModel);
            _repository.Insert(model);
        }

        public virtual ViewModel Insert(ViewModel viewModel)
        {
            var model = Mapper.Map<ViewModel, TEntity>(viewModel);
            return Mapper.Map<TEntity, ViewModel>(_repository.Insert(model));
        }

        public virtual TPrimaryKey InsertAndGetId(ViewModel viewModel)
        {
            var model = Mapper.Map<ViewModel, TEntity>(viewModel);
            return _repository.InsertAndGetId(model);
        }

        public virtual void Update(ViewModel viewModel)
        {
            var model = Mapper.Map<ViewModel, TEntity>(viewModel);
            _repository.Update(model);
        }

        public virtual void Update(ViewModel viewModel, object key)
        {
            var model = Mapper.Map<ViewModel, TEntity>(viewModel);
            _repository.Update(model, key);
        }

        public virtual void Delete(TPrimaryKey id)
        {
            _repository.Delete(id);
        }

        public virtual void Delete(ViewModel entity)
        {
            var model = Mapper.Map<ViewModel, TEntity>(entity);
            _repository.Delete(model);
        }

        public virtual void DeleteMultiple(IEnumerable<ViewModel> entities)
        {
            List<TEntity> list = new List<TEntity>();
            foreach (ViewModel vm in entities)
            {
                var model = Mapper.Map<ViewModel, TEntity>(vm);
                list.Add(model);
            }

            _repository.DeleteRange(list);
        }

        public virtual void DeleteMultiple(List<ViewModel> entities)
        {
            List<TEntity> list = new List<TEntity>();
            foreach (ViewModel vm in entities)
            {
                var model = Mapper.Map<ViewModel, TEntity>(vm);
                list.Add(model);
            }

            _repository.DeleteRange(list);
        }

        public virtual ViewModel GetById(TPrimaryKey id)
        {
            return Mapper.Map<TEntity, ViewModel>(_repository.GetById(id));
        }

        public virtual List<ViewModel> GetAll()
        {
            return _repository.GetAll().ProjectTo<ViewModel>().ToList();
        }

        public virtual PagedResult<ViewModel> GetAllPaging(Expression<Func<TEntity, bool>> predicate, Func<TEntity, object> orderBy,
            SortDirection sortDirection, int pageIndex, int pageSize)
        {
            var query = _repository.GetAll().Where(predicate);

            int totalRow = query.Count();

            if (sortDirection == SortDirection.Ascending)
            {
                query = query.OrderBy(orderBy)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .AsQueryable();
            }
            else if (sortDirection == SortDirection.Descending)
            {
                query = query.OrderByDescending(orderBy)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .AsQueryable();
            }
            else
            {
                query = query.Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .AsQueryable();
            }

            var data = query.ProjectTo<ViewModel>().ToList();
            var paginationSet = new PagedResult<ViewModel>()
            {
                Results = data,
                CurrentPage = pageIndex,
                RowCount = totalRow,
                PageSize = pageSize
            };

            return paginationSet;
        }

        public virtual PagedResult<ViewModel> GetAllPaging(Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize)
        {
            var query = _repository.GetAll().Where(predicate);

            int totalRow = query.Count();

            query = query.Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .AsQueryable();

            var data = query.ProjectTo<ViewModel>().ToList();
            var paginationSet = new PagedResult<ViewModel>()
            {
                Results = data,
                CurrentPage = pageIndex,
                RowCount = totalRow,
                PageSize = pageSize
            };

            return paginationSet;
        }

        public virtual PagedResult<ViewModel> GetAllPaging(int pageIndex, int pageSize)
        {
            var query = _repository.GetAll();

            int totalRow = query.Count();

            query = query.Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .AsQueryable();

            var data = query.ProjectTo<ViewModel>().ToList();
            var paginationSet = new PagedResult<ViewModel>()
            {
                Results = data,
                CurrentPage = pageIndex,
                RowCount = totalRow,
                PageSize = pageSize
            };

            return paginationSet;
        }

        public virtual List<ViewModel> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            return _repository.GetAllIncluding(propertySelectors).ProjectTo<ViewModel>().ToList();
        }

        public virtual List<ViewModel> GetAllIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            return _repository.GetAllIncluding(predicate, propertySelectors).ProjectTo<ViewModel>().ToList();
        }

        public virtual bool CheckExits(object key)
        {
            return _repository.CheckExits(key);
        }

        public virtual bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.Exists(predicate);
        }

        #endregion

        #region Async

        public virtual async Task<int> CountAsync()
        {
            return await _repository.CountAsync();
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _repository.CountAsync(predicate);
        }

        public virtual async Task<long> LongCountAsync()
        {
            return await _repository.LongCountAsync();
        }

        public virtual async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _repository.LongCountAsync(predicate);
        }

        public virtual async Task<ViewModel> GetByAsync(ViewModel entity)
        {
            var model = Mapper.Map<ViewModel, TEntity>(entity);
            var result = await _repository.GetByAsync(model);

            return Mapper.Map<TEntity, ViewModel>(result);
        }

        public virtual async Task<ViewModel> GetByIdAsync(TPrimaryKey id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return Mapper.Map<TEntity, ViewModel>(entity);
        }

        public virtual async Task<IEnumerable<ViewModel>> GetRangeAsync(IEnumerable<ViewModel> entities)
        {
            var list = Mapper.Map<List<ViewModel>, List<TEntity>>(entities.ToList());
            var items = await _repository.GetRangeAsync(list);
            var results = Mapper.Map<List<TEntity>, List<ViewModel>>(items);
            return results;
        }

        public virtual async Task<List<ViewModel>> GetRangeAsync(List<ViewModel> entities)
        {
            var list = Mapper.Map<List<ViewModel>, List<TEntity>>(entities);
            var items = await _repository.GetRangeAsync(list);
            var results = Mapper.Map<List<TEntity>, List<ViewModel>>(items);
            return results;
        }

        public virtual async Task<IEnumerable<ViewModel>> GetRangeAsync(IEnumerable<TPrimaryKey> id)
        {
            var items = await _repository.GetRangeAsync(id);
            var results = Mapper.Map<List<TEntity>, List<ViewModel>>(items.ToList());
            return results;
        }

        public virtual async Task<List<ViewModel>> GetRangeAsync(List<TPrimaryKey> id)
        {
            var items = await _repository.GetRangeAsync(id);
            var results = Mapper.Map<List<TEntity>, List<ViewModel>>(items);
            return results;
        }

        public virtual async Task<List<ViewModel>> GetAllAsync()
        {
            return await _repository.GetAll().ProjectTo<ViewModel>().ToListAsync();
        }

        public virtual async Task<List<ViewModel>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _repository.GetAll().Where(predicate).ProjectTo<ViewModel>().ToListAsync();
        }

        public virtual async Task<List<ViewModel>> GetAllIncludingAsync(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            return await _repository.GetAllIncluding(propertySelectors).ProjectTo<ViewModel>().ToListAsync();
        }

        public virtual async Task<List<ViewModel>> GetAllIncludingAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            return await _repository.GetAllIncluding(predicate, propertySelectors).ProjectTo<ViewModel>().ToListAsync();
        }

        public virtual async Task<ViewModel> InsertAsync(ViewModel entity)
        {
            var model = Mapper.Map<ViewModel, TEntity>(entity);
            var result = await _repository.InsertAsync(model);

            return Mapper.Map<TEntity, ViewModel>(result);
        }

        public virtual async Task<TPrimaryKey> InsertAndGetIdAsync(ViewModel entity)
        {
            var model = Mapper.Map<ViewModel, TEntity>(entity);

            return await _repository.InsertAndGetIdAsync(model);
        }

        public virtual async Task<List<ViewModel>> InsertRangeAsync(List<ViewModel> entities)
        {
            var list = Mapper.Map<List<ViewModel>, List<TEntity>>(entities);
            var items = await _repository.InsertRangeAsync(list);
            var results = Mapper.Map<List<TEntity>, List<ViewModel>>(items);
            return results;
        }

        public virtual async Task<IEnumerable<ViewModel>> InsertRangeAsync(IEnumerable<ViewModel> entities)
        {
            var list = Mapper.Map<List<ViewModel>, List<TEntity>>(entities.ToList());
            var items = await _repository.InsertRangeAsync(list);
            var results = Mapper.Map<List<TEntity>, List<ViewModel>>(items);
            return results;
        }

        public virtual async Task<ViewModel> UpdateAsync(ViewModel entity)
        {
            var model = Mapper.Map<ViewModel, TEntity>(entity);
            var result = await _repository.UpdateAsync(model);
            return Mapper.Map<TEntity, ViewModel>(result);
        }

        public virtual async Task<ViewModel> UpdateAsync(ViewModel entity, object key)
        {
            var model = Mapper.Map<ViewModel, TEntity>(entity);
            var result = await _repository.UpdateAsync(model, key);
            return Mapper.Map<TEntity, ViewModel>(result);
        }

        public virtual async Task<IEnumerable<ViewModel>> UpdateRangeAsync(IEnumerable<ViewModel> entities)
        {
            var list = Mapper.Map<List<ViewModel>, List<TEntity>>(entities.ToList());
            var items = await _repository.UpdateRangeAsync(list);
            var results = Mapper.Map<List<TEntity>, List<ViewModel>>(items);
            return results;
        }

        public virtual async Task<List<ViewModel>> UpdateRangeAsync(List<ViewModel> entities)
        {
            var list = Mapper.Map<List<ViewModel>, List<TEntity>>(entities);
            var items = await _repository.UpdateRangeAsync(list);
            var results = Mapper.Map<List<TEntity>, List<ViewModel>>(items);
            return results;
        }

        public virtual async Task<int> DeleteAsync(TPrimaryKey id)
        {
            return await _repository.DeleteAsync(id);
        }

        public virtual async Task<ViewModel> DeleteAsync(ViewModel entity)
        {
            var model = Mapper.Map<ViewModel, TEntity>(entity);
            await _repository.DeleteAsync(model);
            return entity;
        }

        public virtual async Task<int> DeleteRangeAsync(IEnumerable<TPrimaryKey> id)
        {
            var results = await _repository.DeleteRangeAsync(id);
            return results.Count();
        }

        public virtual async Task<int> DeleteRangeAsync(List<TPrimaryKey> id)
        {
            var results = await _repository.DeleteRangeAsync(id);
            return results.Count();
        }

        public virtual async Task<IEnumerable<ViewModel>> DeleteRangeAsync(IEnumerable<ViewModel> entities)
        {
            var list = Mapper.Map<List<ViewModel>, List<TEntity>>(entities.ToList());
            var items = await _repository.DeleteRangeAsync(list);
            var results = Mapper.Map<List<TEntity>, List<ViewModel>>(items);
            return results;
        }

        public virtual async Task<List<ViewModel>> DeleteRangeAsync(List<ViewModel> entities)
        {
            var list = Mapper.Map<List<ViewModel>, List<TEntity>>(entities.ToList());
            var items = await _repository.DeleteRangeAsync(list);
            var results = Mapper.Map<List<TEntity>, List<ViewModel>>(items);
            return results;
        }

        public virtual async Task<bool> CheckExitsAsync(object key)
        {
            return await _repository.CheckExitsAsync(key);
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _repository.ExistsAsync(predicate);
        }

        public virtual async Task<PagedResult<ViewModel>> GetAllPagingAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> orderBy,
            SortDirection sortDirection, int pageIndex, int pageSize)
        {
            var query = _repository.GetAll(predicate);

            int totalRow = query.Count();

            if (sortDirection == SortDirection.Ascending)
            {
                query = query.OrderBy(orderBy)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .AsQueryable();
            }
            else if (sortDirection == SortDirection.Descending)
            {
                query = query.OrderByDescending(orderBy)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .AsQueryable();
            }
            else
            {
                query = query.Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .AsQueryable();
            }

            var data = await query.ProjectTo<ViewModel>().ToListAsync();
            var paginationSet = new PagedResult<ViewModel>()
            {
                Results = data,
                CurrentPage = pageIndex,
                RowCount = totalRow,
                PageSize = pageSize
            };

            return paginationSet;
        }

        public virtual async Task<PagedResult<ViewModel>> GetAllPagingAsync(Expression<Func<TEntity, bool>> predicate,
            int pageIndex, int pageSize)
        {
            var query = _repository.GetAll(predicate);

            int totalRow = query.Count();

            query = query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .AsQueryable();

            var data = await query.ProjectTo<ViewModel>().ToListAsync();
            var paginationSet = new PagedResult<ViewModel>()
            {
                Results = data,
                CurrentPage = pageIndex,
                RowCount = totalRow,
                PageSize = pageSize
            };

            return paginationSet;
        }

        public virtual async Task<PagedResult<ViewModel>> GetAllPagingAsync(int pageIndex, int pageSize)
        {
            var query = _repository.GetAll();

            int totalRow = query.Count();

            query = query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .AsQueryable();

            var data = await query.ProjectTo<ViewModel>().ToListAsync();
            var paginationSet = new PagedResult<ViewModel>()
            {
                Results = data,
                CurrentPage = pageIndex,
                RowCount = totalRow,
                PageSize = pageSize
            };

            return paginationSet;
        }

        #endregion

        #region Dispose

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
