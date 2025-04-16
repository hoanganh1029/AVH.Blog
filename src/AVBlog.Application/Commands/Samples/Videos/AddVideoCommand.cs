using AVBlog.Application.Responses;
using AVBlog.Application.ViewModels.Samples;
using MediatR;

namespace AVBlog.Application.Commands.Samples.Videos;

public class AddVideoCommand : IRequest<Response>
{
    public required VimeoVideoViewModel VimeoVideoViewModel { get; set; }
}
