using AVBlog.Application.Responses;
using MediatR;

namespace AVBlog.Application.Commands.Samples.Videos;

public class DeleteVideoCommand : IRequest<Response>
{
    public required Guid Id { get; set; }
}
