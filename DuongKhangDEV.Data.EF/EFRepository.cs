using DuongKhangDEV.Data.Interfaces;
using DuongKhangDEV.Infrastructure.Interfaces;
using DuongKhangDEV.Infrastructure.SharedKernel;
using DuongKhangDEV.Utilities.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DuongKhangDEV.Data.EF
{
    public class EFRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>, IDisposable
        where TEntity : DomainEntity<TPrimaryKey>
    {
        protected readonly AppDbContext _dbContext;

        public EFRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Not Async

        public int Count()
        {            
            return _dbContext.Set<TEntity>().Count();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Count(predicate);
        }

        public long LongCount()
        {
            return _dbContext.Set<TEntity>().LongCount();
        }

        public long LongCount(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().LongCount(predicate);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsQueryable();
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate);
        }        

        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            IQueryable<TEntity> items = GetAll();

            if (propertySelectors != null)
            {
                foreach (var includeProperty in propertySelectors)
                {
                    items = items.Include(includeProperty);
                }
            }
            return items;
        }

        public IQueryable<TEntity> GetAllIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            IQueryable<TEntity> items = _dbContext.Set<TEntity>();
            if (propertySelectors != null)
            {
                foreach (var includeProperty in propertySelectors)
                {
                    items = items.Include(includeProperty);
                }
            }
            return items.Where(predicate);
        }

        public List<TEntity> GetAllList()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        public List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate).ToList();
        }

        public TEntity GetById(TPrimaryKey id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public TEntity GetFirst(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().First<TEntity>(predicate);
        }

        public TEntity GetLast(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Last<TEntity>(predicate);
        }

        public TEntity Insert(TEntity entity)
        {
            var result = _dbContext.Set<TEntity>().Add(entity).Entity;
            SaveChanges();
            return result;
        }

        public TPrimaryKey InsertAndGetId(TEntity entity)
        {
            var result = _dbContext.Set<TEntity>().Add(entity);
            SaveChanges();
            return result.Entity.Id;
        }

        public void InsertRange(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().AddRange(entities);
            SaveChanges();
        }

        public void InsertRange(List<TEntity> entities)
        {
            _dbContext.Set<TEntity>().AddRange(entities);
            SaveChanges();
        }

        //public TEntity Update(TEntity entity)
        //{
        //    var result = _dbContext.Set<TEntity>().Update(entity);
        //    SaveChanges();
        //    return result.Entity;
        //}

        public TEntity Update(TEntity entity)
        {
            var dbEntity = _dbContext.Set<TEntity>().AsNoTracking().Single(p => p.Id.Equals(entity.Id));
            var databaseEntry = _dbContext.Entry(dbEntity);
            var inputEntry = _dbContext.Entry(entity);

            //no items mentioned, so find out the updated entries
            IEnumerable<string> dateProperties = typeof(IDateTracking).GetPublicProperties().Select(x => x.Name);

            var allProperties = databaseEntry.Metadata.GetProperties()
            .Where(x => !dateProperties.Contains(x.Name));

            foreach (var property in allProperties)
            {
                var proposedValue = inputEntry.Property(property.Name).CurrentValue;
                var originalValue = databaseEntry.Property(property.Name).OriginalValue;

                if (proposedValue != null && !proposedValue.Equals(originalValue))
                {
                    databaseEntry.Property(property.Name).IsModified = true;
                    databaseEntry.Property(property.Name).CurrentValue = proposedValue;
                }
            }

            var result = _dbContext.Set<TEntity>().Update(dbEntity);
            SaveChanges(); // Đoạn này thêm vào
            return result.Entity;
        }

        public TEntity Update(TEntity entity, object key)
        {
            if (entity == null)
            {
                return null;
            }

            TEntity exist = _dbContext.Set<TEntity>().Find(key);
            if (exist != null)
            {
                var databaseEntry = _dbContext.Entry(exist);
                var inputEntry = _dbContext.Entry(entity);

                //no items mentioned, so find out the updated entries
                IEnumerable<string> dateProperties = typeof(IDateTracking).GetPublicProperties().Select(x => x.Name);

                var allProperties = databaseEntry.Metadata.GetProperties()
                .Where(x => !dateProperties.Contains(x.Name));

                foreach (var property in allProperties)
                {
                    var proposedValue = inputEntry.Property(property.Name).CurrentValue;
                    var originalValue = databaseEntry.Property(property.Name).OriginalValue;

                    if (proposedValue != null && !proposedValue.Equals(originalValue))
                    {
                        databaseEntry.Property(property.Name).IsModified = true;
                        databaseEntry.Property(property.Name).CurrentValue = proposedValue;
                    }
                }
                _dbContext.Entry(exist).CurrentValues.SetValues(entity);
                SaveChanges();
            }

            return exist;
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            SaveChanges();
        }

        public void Delete(TPrimaryKey id)
        {
            _dbContext.Set<TEntity>().Remove(GetById(id));
            SaveChanges();
        }

        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            _dbContext.Set<TEntity>().RemoveRange(_dbContext.Set<TEntity>().Where(predicate));
            SaveChanges();
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
            SaveChanges();
        }

        public void DeleteRange(List<TEntity> entities)
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
            SaveChanges();
        }

        public TEntity FirstOrDefault(TPrimaryKey id)
        {
            return _dbContext.Set<TEntity>().FirstOrDefault(x => x.Id.Equals(id));
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().FirstOrDefault(predicate);
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Single(predicate);
        }

        public bool CheckExits(object key)
        {
            return _dbContext.Set<TEntity>().Find(key) == null;
        }

        public bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Any(predicate);
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }        

        #endregion

        #region Async

        public async Task<int> CountAsync()
        {
            return await _dbContext.Set<TEntity>().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().CountAsync(predicate);
        }        
        
        public async Task<long> LongCountAsync()
        {
            return await _dbContext.Set<TEntity>().LongCountAsync();
        }

        public async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().LongCountAsync(predicate);
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id)
        {
            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            var result = await _dbContext.Set<TEntity>().AddAsync(entity);
            await SaveChangesAsync();
            return result.Entity;
        }

        public async Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
        {
            var result = await _dbContext.Set<TEntity>().AddAsync(entity);
            await SaveChangesAsync();
            return result.Entity.Id;
        }

        public virtual async void InsertRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().AddRange(entities);
            await SaveChangesAsync();
        }

        public virtual async void InsertRangeAsync(List<TEntity> entities)
        {
            _dbContext.Set<TEntity>().AddRange(entities);
            await SaveChangesAsync();
        }

        //public async Task<TEntity> UpdateAsync(TEntity entity)
        //{
        //    var result = _dbContext.Set<TEntity>().Update(entity);
        //    await SaveChangesAsync();
        //    return result.Entity;
        //}

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var dbEntity = _dbContext.Set<TEntity>().AsNoTracking().Single(p => p.Id.Equals(entity.Id));
            var databaseEntry = _dbContext.Entry(dbEntity);
            var inputEntry = _dbContext.Entry(entity);

            //no items mentioned, so find out the updated entries
            IEnumerable<string> dateProperties = typeof(IDateTracking).GetPublicProperties().Select(x => x.Name);

            var allProperties = databaseEntry.Metadata.GetProperties()
            .Where(x => !dateProperties.Contains(x.Name));

            foreach (var property in allProperties)
            {
                var proposedValue = inputEntry.Property(property.Name).CurrentValue;
                var originalValue = databaseEntry.Property(property.Name).OriginalValue;

                if (proposedValue != null && !proposedValue.Equals(originalValue))
                {
                    databaseEntry.Property(property.Name).IsModified = true;
                    databaseEntry.Property(property.Name).CurrentValue = proposedValue;
                }
            }

            var result = _dbContext.Set<TEntity>().Update(dbEntity);
            await SaveChangesAsync(); // Đoạn này thêm vào
            return result.Entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, object key)
        {
            if (entity == null)
            {
                return null;
            }

            TEntity exist = await _dbContext.Set<TEntity>().FindAsync(key);
            if (exist != null)
            {
                var databaseEntry = _dbContext.Entry(exist);
                var inputEntry = _dbContext.Entry(entity);

                //no items mentioned, so find out the updated entries
                IEnumerable<string> dateProperties = typeof(IDateTracking).GetPublicProperties().Select(x => x.Name);

                var allProperties = databaseEntry.Metadata.GetProperties()
                .Where(x => !dateProperties.Contains(x.Name));

                foreach (var property in allProperties)
                {
                    var proposedValue = inputEntry.Property(property.Name).CurrentValue;
                    var originalValue = databaseEntry.Property(property.Name).OriginalValue;

                    if (proposedValue != null && !proposedValue.Equals(originalValue))
                    {
                        databaseEntry.Property(property.Name).IsModified = true;
                        databaseEntry.Property(property.Name).CurrentValue = proposedValue;
                    }
                }

                _dbContext.Entry(exist).CurrentValues.SetValues(exist);
                await SaveChangesAsync();
            }

            return exist;
        }

        public virtual async Task<TEntity> DeleteAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            await SaveChangesAsync();
            return entity;
        }

        public virtual async Task<int> DeleteAsync(TPrimaryKey id)
        {
            TEntity exist = await _dbContext.Set<TEntity>().FindAsync(id);
            if (exist != null)
            {
                _dbContext.Set<TEntity>().Remove(exist);
            }
            return await SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
            await SaveChangesAsync();
            return entities;
        }

        public virtual async Task<List<TEntity>> DeleteRangeAsync(List<TEntity> entities)
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
            await SaveChangesAsync();
            return entities;
        }

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().SingleAsync(predicate);
        }

        public virtual async Task<TEntity> GetByAsync(TEntity entity)
        {
            return await _dbContext.Set<TEntity>().FindAsync(entity);
        }

        public virtual async Task<TEntity> GetByIdAsync(TPrimaryKey id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {            
            return await _dbContext.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<bool> CheckExitsAsync(object key)
        {
            return await _dbContext.Set<TEntity>().FindAsync(key) == null;
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().AnyAsync(predicate);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        #endregion

        #region Dispose

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
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
