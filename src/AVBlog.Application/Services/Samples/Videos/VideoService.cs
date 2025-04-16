using AutoMapper;
using AVBlog.Application.Extensions.Samples;
using AVBlog.Application.Responses;
using AVBlog.Application.Services.Base;
using AVBlog.Application.ViewModels.Samples;
using AVBlog.Domain.Entities.Samples;
using AVBlog.Domain.Repositories.Samples.UserVimeoVideos;
using AVBlog.Domain.Repositories.Samples.VimeoVideos;

namespace AVBlog.Application.Services.Samples.Videos
{
    public class VideoService : BaseService<VimeoVideo, VimeoVideoViewModel>, IVideoService
    {
        private readonly IUserVimeoVideoQueryRepository _userVimeoVideoRepository;

        public VideoService(
            IVimeoVideoQueryRepository queryRepository,
            IVimeoVideoCommandRepository commandRepository,
            IMapper mapper,
            IUserVimeoVideoQueryRepository userVimeoVideoRepository) : base(queryRepository, commandRepository, mapper)
        {
            _userVimeoVideoRepository = userVimeoVideoRepository;
        }

        public async Task<IEnumerable<VimeoVideoViewModel>> GetByUserIdAsync(string userId)
        {
            var videos = await _userVimeoVideoRepository.GetByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<VimeoVideoViewModel>>(videos);
        }

        public override async Task<Response> AddAsync(VimeoVideoViewModel viewModel)
        {
            var queryRepo = _queryRepository as IVimeoVideoQueryRepository;
            var isDuplicate = await queryRepo!.IsVimeoIdExist(viewModel.VimeoId);
            if (isDuplicate)
            {
                return BadRequest("VimeoId is already exist");
            }

            await base.AddAsync(viewModel);
            return Success();
        }

        public override async Task<Response> UpdateAsync(Guid id, VimeoVideoViewModel viewModel)
        {
            var isExist = await IsExistAsync(id);
            if (!isExist)
            {
                return NotFound();
            }

            var queryRepo = _queryRepository as IVimeoVideoQueryRepository;
            var isDuplicate = await queryRepo!.IsVimeoIdExist(viewModel.VimeoId, viewModel.Id);
            if (isDuplicate)
            {
                return BadRequest("VimeoId is already exist");
            }

            await base.UpdateAsync(id, viewModel);
            return Success();
        }

        protected override void UpdateEntityFromViewModel(VimeoVideoViewModel viewModel, VimeoVideo entity)
        {
            entity.UpdateFromModel(viewModel);
        }
    }
}
