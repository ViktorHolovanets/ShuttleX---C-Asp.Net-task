using My_one_day_life_api.Helpers.Classes;

namespace My_one_day_life_api.Repositories.Interfaces
{
    public interface IEntityRepository<TEntity>
    {
        Task<TEntity> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync(PaginationInfo pagination);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(Guid id);
        IQueryable<TEntity> BuildQuery();
    }
}
