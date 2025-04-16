using AutoMapper;
using AVBlog.Application.Responses;
using AVBlog.Domain.Constants;
using AVBlog.Domain.Entities.Base;
using AVBlog.Domain.Repositories.Base;
using System.Linq.Expressions;

namespace AVBlog.Application.Queries.Base
{
    public abstract class QueryHandlerBase<TEntity, TModel> : GeneralServiceResponseAttributes,
        IQueryHandlerBase<TEntity, TModel> where TEntity : BaseEntity
    {
        protected readonly IBaseQueryRepository<TEntity> _mainRepository;
        protected readonly IMapper _mapper;
        public QueryHandlerBase(IBaseQueryRepository<TEntity> mainRepository, IMapper mapper)
        {
            _mainRepository = mainRepository;
            _mapper = mapper;
        }

        public virtual async Task<TModel?> GetByIdAsync(Guid id)
        {
            var entity = await _mainRepository.GetByIdAsync(id);
            return _mapper.Map<TModel?>(entity);
        }

        public virtual async Task<IEnumerable<TModel>> GetAllAsync()
        {
            var entities = await _mainRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TModel>>(entities);
        }

        public virtual async Task<bool> IsExistAsync(Guid id)
        {
            return await _mainRepository.IsExistAsync(id);
        }

        public virtual async Task<IEnumerable<TModel>> GetAllWithOrderAsync(Expression<Func<TEntity, DateTime?>>? orderBy = null, OrderType orderType = OrderType.Descending)
        {
            var entities = await _mainRepository.GetAllWithOrderAsync(orderBy, orderType);
            return _mapper.Map<IEnumerable<TModel>>(entities);
        }

        public virtual async Task<IEnumerable<TModel>> GetAllWithOrderAsync(Expression<Func<TEntity, string?>>? orderBy = null, OrderType orderType = OrderType.Descending)
        {
            var entities = await _mainRepository.GetAllWithOrderAsync(orderBy, orderType);
            return _mapper.Map<IEnumerable<TModel>>(entities);
        }
    }
}
