using AVBlog.Domain.Constants;
using AVBlog.Domain.Entities.Base;
using AVBlog.Domain.Repositories.Base;
using AVBlog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AVBlog.Infrastructure.Repositories.Base
{
    public abstract class BaseQueryRepository<TEntity> : IBaseQueryRepository<TEntity> where TEntity : BaseEntity
    {
        protected AVBlogQueryContext _context;
        protected DbSet<TEntity> _currentEntity;
        public BaseQueryRepository(AVBlogQueryContext dbContext)
        {
            _context = dbContext;
            _currentEntity = _context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _currentEntity.AsNoTracking().ToListAsync();
        }

        public virtual async Task<TEntity?> GetByIdAsync(Guid id)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await _currentEntity.FindAsync(id);
        }

        public async Task<bool> IsExistAsync(Guid id)
        {
            return await _currentEntity.AnyAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<TEntity>> GetAllWithOrderAsync(Expression<Func<TEntity, DateTime?>>? orderBy = null, OrderType orderType = OrderType.Descending)
        {
            if (orderBy == null)
            {
                return await GetAllAsync();
            }
            var queryable = _currentEntity.AsNoTracking();
            queryable = orderType switch
            {
                OrderType.Ascending => queryable.OrderBy(orderBy),
                OrderType.Descending => queryable.OrderByDescending(orderBy),
                _ => queryable
            };
            return await queryable.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllWithOrderAsync(Expression<Func<TEntity, string?>>? orderBy = null, OrderType orderType = OrderType.Descending)
        {
            if (orderBy == null)
            {
                return await GetAllAsync();
            }
            var queryable = _currentEntity.AsNoTracking();
            queryable = orderType switch
            {
                OrderType.Ascending => queryable.OrderBy(orderBy),
                OrderType.Descending => queryable.OrderByDescending(orderBy),
                _ => queryable
            };
            return await queryable.ToListAsync();
        }
    }
}
