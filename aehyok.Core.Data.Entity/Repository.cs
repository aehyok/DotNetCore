using aehyok.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace aehyok.Core.Data.Entity
{
    /// <summary>
    /// EntityFramework的仓储实现
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : EntityBase<TKey>
    {
        private readonly DbSet<TEntity> _dbSet;

        private readonly IUnitOfWork _unitOfWork;



        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _dbSet = ((DbContext)unitOfWork).Set<TEntity>();
        }

        public IUnitOfWork UnitOfWork { get { return _unitOfWork; } }

        public IQueryable<TEntity> Entities { get { return _dbSet.AsNoTracking(); } }

        public async Task<bool> CheckExistsAsync(Expression<Func<TEntity, bool>> predicate, TKey id = default(TKey))
        {
            TKey defaultId = default(TKey);
            var entity = await _dbSet.Where(predicate).Select(m => new { m.Id }).SingleOrDefaultAsync();
            bool exists = (id.Equals(null)) || id.Equals(defaultId)
                ? entity != null
                : entity != null && !entity.Id.Equals(id);
            return exists;
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            //_dbSet.Remove(entity);
            entity.IsDeleted = true;
            _dbSet.Update(entity);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(TKey key)
        {
            TEntity entity = _dbSet.Find(key);
            return  entity == null ? 0 : await DeleteAsync(entity);
        }

        public async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            TEntity[] entities = _dbSet.Where(predicate).ToArray();
            return entities.Length == 0 ? 0 :await DeleteAsync(entities);
        }

        public async Task<int> DeleteAsync(IEnumerable<TEntity> entities)
        {
            //_dbSet.RemoveRange(entities);
            foreach(var entity in entities)
            {
                entity.IsDeleted = true;
            }
            _dbSet.UpdateRange(entities);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<TEntity> GetByKeyAsync(TKey key)
        {
            return await _dbSet.FindAsync(key);
        }

        public IQueryable<TEntity> GetInclude<TProperty>(Expression<Func<TEntity, TProperty>> path)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> GetIncludes(params string[] paths)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            return _unitOfWork.SaveChangesAsync();
        }

        public Task<int> InsertAsync(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
            return _unitOfWork.SaveChangesAsync();
        }

        public Task<int> UpdateAsync(TEntity entity)
        {
            ((DbContext)_unitOfWork).Update(entity);
            return _unitOfWork.SaveChangesAsync();
        }

        #region 私有方法

        private int SaveChanges()
        {
            return _unitOfWork.TransactionEnabled ? 0 : _unitOfWork.SaveChanges();
        }

        //#if NET45

        private async Task<int> SaveChangesAsync()
        {
            return _unitOfWork.TransactionEnabled ? 0 : await _unitOfWork.SaveChangesAsync();
        }

        //#endif
        #endregion
    }

}
