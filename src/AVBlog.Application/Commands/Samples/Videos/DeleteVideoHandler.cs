using AutoMapper;
using AVBlog.Application.Commands.Base;
using AVBlog.Application.Responses;
using AVBlog.Application.ViewModels.Samples;
using AVBlog.Domain.Entities.Samples;
using AVBlog.Domain.Repositories.Base;
using MediatR;

namespace AVBlog.Application.Commands.Samples.Videos;

public class DeleteVideoHandler(IBaseCommandRepository<VimeoVideo> commandRepository, IBaseQueryRepository<VimeoVideo> queryRepository, IMapper mapper)
    : CommandHandlerBase<VimeoVideo, VimeoVideoViewModel>(commandRepository, queryRepository, mapper),
    IRequestHandler<DeleteVideoCommand, Response>
{
    public async Task<Response> Handle(DeleteVideoCommand request, CancellationToken cancellationToken)
    {
        var isExist = await _queryRepository.IsExistAsync(request.Id);
        if (!isExist)
        {
            return NotFound();
        }

        await base.DeleteAsync(request.Id);
        return Success();
    }
}