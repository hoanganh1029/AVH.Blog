using AVBlog.Application.Responses;
using AVBlog.Application.ViewModels.Samples;
using MediatR;

namespace AVBlog.Application.Commands.Samples.Videos;

public class UpdateVideoCommand : IRequest<Response>
{
    public required Guid Id { get; set; }
    public required VimeoVideoViewModel VimeoVideoViewModel { get; set; }
}
