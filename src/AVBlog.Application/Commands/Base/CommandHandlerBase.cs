using AutoMapper;
using AVBlog.Application.Responses;
using AVBlog.Domain.Entities.Base;
using AVBlog.Domain.Repositories.Base;

namespace AVBlog.Application.Commands.Base
{
    public abstract class CommandHandlerBase<TEntity, TModel> : GeneralServiceResponseAttributes,
        ICommandHandlerBase<TEntity, TModel> where TEntity : BaseEntity
    {
        protected readonly IBaseCommandRepository<TEntity> _commandRepository;
        protected readonly IBaseQueryRepository<TEntity> _queryRepository;
        protected readonly IMapper _mapper;
        public CommandHandlerBase(
            IBaseCommandRepository<TEntity> commandRepository,
            IBaseQueryRepository<TEntity> queryRepository,
            IMapper mapper)
        {
            _commandRepository = commandRepository;
            _queryRepository = queryRepository;
            _mapper = mapper;
        }

        public virtual async Task<TEntity> AddAsync(TModel viewModel)
        {
            var entity = _mapper.Map<TEntity>(viewModel);
            entity.Id = Guid.NewGuid();
            await _commandRepository.AddAsync(entity);
            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(Guid id, TModel viewModel)
        {
            var entity = await _queryRepository.GetByIdAsync(id) ?? throw new NullReferenceException($"Entity {typeof(TEntity)} is null");
            UpdateEntityFromViewModel(viewModel, entity);
            await _commandRepository.UpdateAsync(entity);
            return entity;
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await _queryRepository.GetByIdAsync(id);
            if (entity != null)
            {
                await _commandRepository.DeleteAsync(entity);
            }
        }

        protected virtual void UpdateEntityFromViewModel(TModel viewModel, TEntity entity)
        {

        }
    }
}
