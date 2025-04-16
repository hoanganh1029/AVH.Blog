using AutoMapper;
using AVBlog.Application.Queries.Base;
using AVBlog.Application.ViewModels.Samples;
using AVBlog.Domain.Entities.Samples;
using AVBlog.Domain.Repositories.Samples.VimeoVideos;
using MediatR;

namespace AVBlog.Application.Queries.Samples.Videos;

public class GetAllWithOrderHandler(IVimeoVideoQueryRepository mainRepository, IMapper mapper)
    : QueryHandlerBase<VimeoVideo, VimeoVideoViewModel>(mainRepository, mapper),
    IRequestHandler<GetAllWithOrderQuery, IEnumerable<VimeoVideoViewModel>>
{
    public async Task<IEnumerable<VimeoVideoViewModel>> Handle(GetAllWithOrderQuery request, CancellationToken cancellationToken)
    {
        return await GetAllWithOrderAsync(request.OrderBy, request.OrderType);
    }
}
