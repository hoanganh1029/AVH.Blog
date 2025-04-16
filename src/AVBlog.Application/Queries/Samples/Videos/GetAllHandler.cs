using AutoMapper;
using AVBlog.Application.Queries.Base;
using AVBlog.Application.ViewModels.Samples;
using AVBlog.Domain.Entities.Samples;
using AVBlog.Domain.Repositories.Samples.VimeoVideos;

namespace AVBlog.Application.Queries.Samples.Videos;

public class GetAllHandler(IVimeoVideoQueryRepository mainRepository, IMapper mapper)
    : QueryHandlerBase<VimeoVideo, VimeoVideoViewModel>(mainRepository, mapper)
    //IRequestHandler<GetAllQuery, IEnumerable<VimeoVideoViewModel>>
{
    public async Task<IEnumerable<VimeoVideoViewModel>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        return await GetAllAsync();
    }
}
