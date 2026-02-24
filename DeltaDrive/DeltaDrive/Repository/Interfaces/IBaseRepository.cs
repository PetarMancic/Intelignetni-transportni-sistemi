using System.Linq.Expressions;

namespace DeltaDrive.Repository.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(int id);

        Task<List<TEntity>> GetAllAsync();

        Task AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
