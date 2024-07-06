using ShuttleX_task_api.Helpers.Classes;

namespace ShuttleX_task_api.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity>
    {
        Task<TEntity> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(PaginationInfo pagination);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(Guid id);
        IQueryable<TEntity> BuildQuery();
    }
}
