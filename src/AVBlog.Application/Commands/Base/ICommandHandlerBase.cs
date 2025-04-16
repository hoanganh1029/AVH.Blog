namespace AVBlog.Application.Commands.Base
{
    public interface ICommandHandlerBase<TEntity, TModel>
    {
        Task<TEntity> AddAsync(TModel entity);
        Task<TEntity> UpdateAsync(Guid id, TModel entity);
        Task DeleteAsync(Guid id);
    }
}
