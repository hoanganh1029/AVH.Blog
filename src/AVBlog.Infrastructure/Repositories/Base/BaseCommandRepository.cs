using AVBlog.Domain.Entities.Base;
using AVBlog.Domain.Repositories.Base;
using AVBlog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AVBlog.Infrastructure.Repositories.Base
{
    public abstract class BaseCommandRepository<TEntity> : IBaseCommandRepository<TEntity> where TEntity : BaseEntity
    {
        protected AVBlogCommandContext _context;
        protected DbSet<TEntity> _currentEntity;
        public BaseCommandRepository(AVBlogCommandContext dbContext)
        {
            _context = dbContext;
            _currentEntity = _context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            _currentEntity.Add(entity);
            await SaveChangeAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _currentEntity.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await SaveChangeAsync();
        }
        public async Task DeleteAsync(TEntity entity)
        {
            _currentEntity.Remove(entity);
            await SaveChangeAsync();
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
