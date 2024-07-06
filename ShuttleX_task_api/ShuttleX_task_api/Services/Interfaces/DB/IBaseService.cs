using ShuttleX_task_api.Helpers.Classes;
using System.Linq.Expressions;

namespace ShuttleX_task_api.Services.Interfaces.DB
{
    public interface IBaseService<TEntity>
    {
        Task<TEntity> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(PaginationInfo pagination);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(Guid id);

        /// <summary>
        /// Build database query
        /// Готує запит до бази даних
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> BuildQuery();
        IQueryable<TEntity> BuildQuery(Expression<Func<TEntity, bool>> filter, PaginationInfo pagination);
    }
}
