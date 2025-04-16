using AutoMapper;
using AVBlog.Application.Commands.Base;
using AVBlog.Application.Responses;
using AVBlog.Application.ViewModels.Samples;
using AVBlog.Domain.Entities.Samples;
using AVBlog.Domain.Repositories.Base;
using AVBlog.Domain.Repositories.Samples.VimeoVideos;
using MediatR;

namespace AVBlog.Application.Commands.Samples.Videos;

public class AddVideoHandler(IBaseCommandRepository<VimeoVideo> commandRepository, IBaseQueryRepository<VimeoVideo> queryRepository, IMapper mapper)
    : CommandHandlerBase<VimeoVideo, VimeoVideoViewModel>(commandRepository, queryRepository, mapper),
    IRequestHandler<AddVideoCommand, Response>
{
    public async Task<Response> Handle(AddVideoCommand request, CancellationToken cancellationToken)
    {
        var queryRepo = _queryRepository as IVimeoVideoQueryRepository;
        var viewModel = request.VimeoVideoViewModel;
        var isDuplicate = await queryRepo!.IsVimeoIdExist(viewModel.VimeoId);
        if (isDuplicate)
        {
            return BadRequest("VimeoId is already exist");
        }

        var video = await base.AddAsync(viewModel);
        var videoViewModel = _mapper.Map<VimeoVideoViewModel>(video);
        return Success(videoViewModel);
    }
}
