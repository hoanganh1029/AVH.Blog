using AutoMapper;
using AVBlog.Application.Queries.Base;
using AVBlog.Application.ViewModels.Samples;
using AVBlog.Domain.Entities.Samples;
using AVBlog.Domain.Repositories.Samples.UserVimeoVideos;
using AVBlog.Domain.Repositories.Samples.VimeoVideos;
using MediatR;

namespace AVBlog.Application.Queries.Samples.Videos;

public class GetByUserIdHandler(
        IVimeoVideoQueryRepository mainRepository,
        IMapper mapper,
        IUserVimeoVideoQueryRepository userVimeoVideoRepository) : QueryHandlerBase<VimeoVideo, VimeoVideoViewModel>(mainRepository, mapper),
    IRequestHandler<GetByUserIdQuery, IEnumerable<VimeoVideoViewModel>>
{
    private readonly IUserVimeoVideoQueryRepository _userVimeoVideoRepository = userVimeoVideoRepository;

    public async Task<IEnumerable<VimeoVideoViewModel>> Handle(GetByUserIdQuery request, CancellationToken cancellationToken)
    {
        var videos = await _userVimeoVideoRepository.GetByUserIdAsync(request.UserId);
        return _mapper.Map<IEnumerable<VimeoVideoViewModel>>(videos);
    }
}
