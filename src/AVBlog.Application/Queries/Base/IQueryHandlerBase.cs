using AVBlog.Domain.Constants;
using System.Linq.Expressions;

namespace AVBlog.Application.Queries.Base
{
    public interface IQueryHandlerBase<TEntity, TModel>
    {
        Task<TModel?> GetByIdAsync(Guid id);
        Task<IEnumerable<TModel>> GetAllAsync();
        Task<bool> IsExistAsync(Guid id);        
        Task<IEnumerable<TModel>> GetAllWithOrderAsync(Expression<Func<TEntity, DateTime?>>? orderBy = null, OrderType orderType = OrderType.Descending);
        Task<IEnumerable<TModel>> GetAllWithOrderAsync(Expression<Func<TEntity, string?>>? orderBy = null, OrderType orderType = OrderType.Descending);

    }
}
