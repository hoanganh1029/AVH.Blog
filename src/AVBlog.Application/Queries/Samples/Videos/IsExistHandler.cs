using AutoMapper;
using AVBlog.Application.Queries.Base;
using AVBlog.Application.ViewModels.Samples;
using AVBlog.Domain.Entities.Samples;
using AVBlog.Domain.Repositories.Samples.VimeoVideos;
using MediatR;

namespace AVBlog.Application.Queries.Samples.Videos;

public class IsExistHandler(IVimeoVideoQueryRepository mainRepository, IMapper mapper)
    : QueryHandlerBase<VimeoVideo, VimeoVideoViewModel?>(mainRepository, mapper),
    IRequestHandler<IsExistQuery, bool>
{
    public async Task<bool> Handle(IsExistQuery request, CancellationToken cancellationToken)
    {
        return await IsExistAsync(request.Id);
    }
}
