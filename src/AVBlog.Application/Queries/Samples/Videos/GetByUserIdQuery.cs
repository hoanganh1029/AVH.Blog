using AVBlog.Application.ViewModels.Samples;
using MediatR;

namespace AVBlog.Application.Queries.Samples.Videos;

public class GetByUserIdQuery : IRequest<IEnumerable<VimeoVideoViewModel>>
{
    public string UserId { get; set; }
}
