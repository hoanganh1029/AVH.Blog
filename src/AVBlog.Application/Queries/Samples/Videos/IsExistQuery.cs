using MediatR;

namespace AVBlog.Application.Queries.Samples.Videos;

public class IsExistQuery : IRequest<bool>
{
    public Guid Id { get; set; }
}
