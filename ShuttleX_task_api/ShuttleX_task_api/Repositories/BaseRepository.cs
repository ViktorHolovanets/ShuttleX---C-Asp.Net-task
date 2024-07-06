using Microsoft.EntityFrameworkCore;
using ShuttleX_task_api.Helpers.Classes;
using ShuttleX_task_api.Models;
using ShuttleX_task_api.Models.Base;
using ShuttleX_task_api.Repositories.Interfaces;
using System.Linq.Expressions;

namespace ShuttleX_task_api.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly AppDb _context;

        public BaseRepository(AppDb context)
        {
            _context = context;
        }

        public async Task<TEntity> GetByIdAsync(Guid id) => await BuildQuery().FirstOrDefaultAsync(entity => entity.Id == id);
        public async Task<IEnumerable<TEntity>> GetAllAsync() => await BuildQuery().ToListAsync();


        public async Task<IEnumerable<TEntity>> GetAllAsync(PaginationInfo pagination)
        {
            var totalEntities = await BuildQuery().ToListAsync();
            var totalCount = totalEntities.Count;

            var totalPages = (int)Math.Ceiling((double)totalCount / pagination.Size);

            if (pagination.Page <= 0 || pagination.Page > totalPages)
            {
                return Enumerable.Empty<TEntity>();
            }

            var entities = await _context.Set<TEntity>()
                .Skip((pagination.Page - 1) * pagination.Size)
                .Take(pagination.Size)
                .ToListAsync();

            return entities;
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public virtual IQueryable<TEntity> BuildQuery()
        {
            return _context.Set<TEntity>().AsQueryable();
        }
        public IQueryable<TEntity> BuildQuery(IEnumerable<Expression<Func<TEntity, bool>>> filters, PaginationInfo pagination)
        {
            var combinedFilter = filters.Aggregate<Expression<Func<TEntity, bool>>, Expression<Func<TEntity, bool>>>(null, (current, filter) => current == null ? filter : Expression.Lambda<Func<TEntity, bool>>(Expression.AndAlso(current.Body, filter.Body), current.Parameters));

            var query = _context.Set<TEntity>().AsQueryable();

            if (combinedFilter != null)
            {
                query = query.Where(combinedFilter);
            }

            query = query.SkipWhile((entity, index) => index < (pagination.Page - 1) * pagination.Size).TakeWhile((entity, index) => index < pagination.Page * pagination.Size);

            return query;
        }

        
    }
}
