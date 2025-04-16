using System.Linq.Expressions;
using AVBlog.Application.ViewModels.Samples;
using AVBlog.Domain.Constants;
using AVBlog.Domain.Entities.Samples;
using MediatR;

namespace AVBlog.Application.Queries.Samples.Videos;

public class GetAllWithOrderQuery : IRequest<IEnumerable<VimeoVideoViewModel>>
{
    public Expression<Func<VimeoVideo, DateTime?>>? OrderBy { get; set; }
    public OrderType OrderType { get; set; } = OrderType.Descending;
}
