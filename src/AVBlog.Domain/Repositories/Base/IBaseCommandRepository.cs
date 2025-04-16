using AVBlog.Domain.Entities.Base;

namespace AVBlog.Domain.Repositories.Base
{
    public interface IBaseCommandRepository<TEntity> where TEntity : BaseEntity
    {
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
