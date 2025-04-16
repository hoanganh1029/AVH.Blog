using AVBlog.Application.ViewModels.Samples;
using MediatR;

namespace AVBlog.Application.Queries.Samples.Videos;

public class GetAllQuery : IRequest<IEnumerable<VimeoVideoViewModel>>
{

}
