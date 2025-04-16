using AVBlog.Domain.Constants;
using AVBlog.Domain.Entities.Base;
using System.Linq.Expressions;

namespace AVBlog.Domain.Repositories.Base
{
    public interface IBaseQueryRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<bool> IsExistAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllWithOrderAsync(Expression<Func<TEntity, DateTime?>>? orderBy = null, OrderType orderType = OrderType.Descending);
        Task<IEnumerable<TEntity>> GetAllWithOrderAsync(Expression<Func<TEntity, string?>>? orderBy = null, OrderType orderType = OrderType.Descending);
    }
}
