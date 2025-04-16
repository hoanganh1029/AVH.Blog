using AVBlog.Domain.Constants;
using System.Linq.Expressions;

namespace AVBlog.Application.Services.Base
{
    public interface IBaseService<TEntity, TModel>
    {
        #region Query
        Task<TModel?> GetByIdAsync(Guid id);
        Task<IEnumerable<TModel>> GetAllAsync();
        Task<bool> IsExistAsync(Guid id);       
        Task<IEnumerable<TModel>> GetAllWithOrderAsync(Expression<Func<TEntity, DateTime?>>? orderBy = null, OrderType orderType = OrderType.Descending);
        Task<IEnumerable<TModel>> GetAllWithOrderAsync(Expression<Func<TEntity, string?>>? orderBy = null, OrderType orderType = OrderType.Descending);
        #endregion

        #region Command
        Task AddAsync(TModel entity);
        Task UpdateAsync(Guid id, TModel entity);
        Task DeleteAsync(Guid id);
        #endregion
    }
}
