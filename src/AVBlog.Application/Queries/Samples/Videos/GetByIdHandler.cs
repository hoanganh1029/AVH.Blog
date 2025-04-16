using AutoMapper;
using AVBlog.Application.Queries.Base;
using AVBlog.Application.ViewModels.Samples;
using AVBlog.Domain.Entities.Samples;
using AVBlog.Domain.Repositories.Samples.VimeoVideos;
using MediatR;

namespace AVBlog.Application.Queries.Samples.Videos;

public class GetByIdHandler(IVimeoVideoQueryRepository mainRepository, IMapper mapper)
    : QueryHandlerBase<VimeoVideo, VimeoVideoViewModel?>(mainRepository, mapper),
    IRequestHandler<GetByIdQuery, VimeoVideoViewModel?>
{
    public async Task<VimeoVideoViewModel?> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        return await GetByIdAsync(request.Id);
    }
}
