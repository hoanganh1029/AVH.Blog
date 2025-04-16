using AutoMapper;
using AVBlog.Application.ViewModels.Samples;
using AVBlog.Domain.Repositories.Samples.VimeoVideos;
using MediatR;

namespace AVBlog.Application.Queries.Samples.Videos;

/// <summary>
/// The handler is used as POC for using Dapper to get data
/// Dapper is more convenient and efficient in certain scenarios (e.g. complex query, report).
/// </summary>
public class GetAllHandlerDapper(IVimeoVideoDapperRepository repository, IMapper mapper)
        : IRequestHandler<GetAllQuery, IEnumerable<VimeoVideoViewModel>>
{
    protected readonly IVimeoVideoDapperRepository _dapperRepository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<VimeoVideoViewModel>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var vimeoVideos = await _dapperRepository.GetAll();
        return _mapper.Map<IEnumerable<VimeoVideoViewModel>>(vimeoVideos);
    }
}
