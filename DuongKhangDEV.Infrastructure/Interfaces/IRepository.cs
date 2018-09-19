using DuongKhangDEV.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DuongKhangDEV.Infrastructure.Interfaces
{
    public interface IRepository<TEntity, TPrimaryKey> where TEntity : DomainEntity<TPrimaryKey>
    {
        #region Not Async

        int Count();

        int Count(Expression<Func<TEntity, bool>> predicate);

        long LongCount();

        long LongCount(Expression<Func<TEntity, bool>> predicate);

        TEntity Insert(TEntity entity);

        TPrimaryKey InsertAndGetId(TEntity entity);

        void InsertRange(IEnumerable<TEntity> entities);

        void InsertRange(List<TEntity> entities);

        TEntity Update(TEntity entity);

        TEntity Update(TEntity entity, object key);

        void Delete(TEntity entity);

        void Delete(TPrimaryKey id);

        void Delete(Expression<Func<TEntity, bool>> predicate);

        void DeleteRange(IEnumerable<TEntity> entities);

        void DeleteRange(List<TEntity> entities);

        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors);

        IQueryable<TEntity> GetAllIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] propertySelectors);

        List<TEntity> GetAllList();

        List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);

        TEntity FirstOrDefault(TPrimaryKey id);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        TEntity GetById(TPrimaryKey id);

        TEntity GetFirst(Expression<Func<TEntity, bool>> predicate);

        TEntity GetLast(Expression<Func<TEntity, bool>> predicate);

        TEntity Single(Expression<Func<TEntity, bool>> predicate);

        bool CheckExits(object key);

        bool Exists(Expression<Func<TEntity, bool>> predicate);

        int SaveChanges();

        #endregion

        #region Async

        Task<int> CountAsync();

        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

        Task<long> LongCountAsync();

        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task<List<TEntity>> GetAllAsync();

        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> GetByIdAsync(TPrimaryKey id);

        Task<TEntity> GetByAsync(TEntity entity);

        Task<IEnumerable<TEntity>> GetRangeAsync(IEnumerable<TEntity> entities);

        Task<List<TEntity>> GetRangeAsync(List<TEntity> entities);

        Task<IEnumerable<TEntity>> GetRangeAsync(IEnumerable<TPrimaryKey> id);

        Task<List<TEntity>> GetRangeAsync(List<TPrimaryKey> id);

        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> InsertAsync(TEntity entity);

        Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity);

        Task<IEnumerable<TEntity>> InsertRangeAsync(IEnumerable<TEntity> entities);

        Task<List<TEntity>> InsertRangeAsync(List<TEntity> entities);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity, object key);

        Task<List<TEntity>> UpdateRangeAsync(List<TEntity> entities);

        Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entities);

        Task<TEntity> DeleteAsync(TEntity entity);

        Task<int> DeleteAsync(TPrimaryKey id);

        Task<IEnumerable<TEntity>> DeleteRangeAsync(IEnumerable<TPrimaryKey> id);

        Task<List<TEntity>> DeleteRangeAsync(List<TPrimaryKey> id);

        Task<IEnumerable<TEntity>> DeleteRangeAsync(IEnumerable<TEntity> entities);

        Task<List<TEntity>> DeleteRangeAsync(List<TEntity> entities);

        Task<bool> CheckExitsAsync(object key);

        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);

        Task<int> SaveChangesAsync();

        #endregion
    }
}
