using AVBlog.Application.ViewModels.Samples;
using MediatR;

namespace AVBlog.Application.Queries.Samples.Videos;

public class GetByIdQuery : IRequest<VimeoVideoViewModel>
{
    public Guid Id { get; set; }
}
