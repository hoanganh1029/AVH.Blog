using AutoMapper;
using AVBlog.Application.Responses;
using AVBlog.Domain.Constants;
using AVBlog.Domain.Entities.Base;
using AVBlog.Domain.Repositories.Base;
using System.Linq.Expressions;

namespace AVBlog.Application.Services.Base
{
    public abstract class BaseService<TEntity, TModel> : GeneralServiceResponseAttributes, IBaseService<TEntity, TModel> where TEntity : BaseEntity
    {
        protected readonly IBaseQueryRepository<TEntity> _queryRepository;
        protected readonly IBaseCommandRepository<TEntity> _commandRepository;
        protected readonly IMapper _mapper;
        public BaseService(
            IBaseQueryRepository<TEntity> queryRepository,
            IBaseCommandRepository<TEntity> commandRepository,
            IMapper mapper)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
            _mapper = mapper;
        }

        #region
        public virtual async Task<TModel?> GetByIdAsync(Guid id)
        {
            var entity = await _queryRepository.GetByIdAsync(id);
            return _mapper.Map<TModel?>(entity);
        }

        public virtual async Task<IEnumerable<TModel>> GetAllAsync()
        {
            var entities = await _queryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TModel>>(entities);
        }

        public virtual async Task<bool> IsExistAsync(Guid id)
        {
            return await _queryRepository.IsExistAsync(id);
        }

        public virtual async Task<IEnumerable<TModel>> GetAllWithOrderAsync(Expression<Func<TEntity, DateTime?>>? orderBy = null, OrderType orderType = OrderType.Descending)
        {
            var entities = await _queryRepository.GetAllWithOrderAsync(orderBy, orderType);
            return _mapper.Map<IEnumerable<TModel>>(entities);
        }

        public virtual async Task<IEnumerable<TModel>> GetAllWithOrderAsync(Expression<Func<TEntity, string?>>? orderBy = null, OrderType orderType = OrderType.Descending)
        {
            var entities = await _queryRepository.GetAllWithOrderAsync(orderBy, orderType);
            return _mapper.Map<IEnumerable<TModel>>(entities);
        }
        #endregion

        #region Command
        public virtual async Task AddAsync(TModel viewModel)
        {
            var entity = _mapper.Map<TEntity>(viewModel);
            entity.Id = Guid.NewGuid();
            await _commandRepository.AddAsync(entity);
        }

        public virtual async Task UpdateAsync(Guid id, TModel viewModel)
        {
            var entity = await _queryRepository.GetByIdAsync(id) ?? throw new NullReferenceException($"Entity {typeof(TEntity)} is null"); ;
            UpdateEntityFromViewModel(viewModel, entity);
            await _commandRepository.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await _queryRepository.GetByIdAsync(id);
            if (entity != null)
            {
                await _commandRepository.DeleteAsync(entity);
            }
        }

        protected abstract void UpdateEntityFromViewModel(TModel viewModel, TEntity entity);

        #endregion
    }
}
