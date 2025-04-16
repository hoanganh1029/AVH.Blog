using AutoMapper;
using AVBlog.Application.Commands.Base;
using AVBlog.Application.Extensions.Samples;
using AVBlog.Application.Responses;
using AVBlog.Application.ViewModels.Samples;
using AVBlog.Domain.Entities.Samples;
using AVBlog.Domain.Repositories.Base;
using AVBlog.Domain.Repositories.Samples.VimeoVideos;
using MediatR;

namespace AVBlog.Application.Commands.Samples.Videos;

public class UpdateVideoHandler(IBaseQueryRepository<VimeoVideo> queryRepository, IBaseCommandRepository<VimeoVideo> commandRepository, IMapper mapper)
    : CommandHandlerBase<VimeoVideo, VimeoVideoViewModel>(commandRepository, queryRepository, mapper),
    IRequestHandler<UpdateVideoCommand, Response>
{
    public async Task<Response> Handle(UpdateVideoCommand request, CancellationToken cancellationToken)
    {
        var isExist = await _queryRepository.IsExistAsync(request.Id);
        if (!isExist)
        {
            return NotFound();
        }

        var queryRepo = _queryRepository as IVimeoVideoQueryRepository;
        var viewModel = request.VimeoVideoViewModel;
        var isDuplicate = await queryRepo!.IsVimeoIdExist(viewModel.VimeoId, viewModel.Id);
        if (isDuplicate)
        {
            return BadRequest("VimeoId is already exist");
        }

        await base.UpdateAsync(request.Id, viewModel);
        return Success();
    }


    protected override void UpdateEntityFromViewModel(VimeoVideoViewModel viewModel, VimeoVideo entity)
    {
        entity.UpdateFromModel(viewModel);
    }
}
