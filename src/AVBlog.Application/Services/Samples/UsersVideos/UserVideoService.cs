using AutoMapper;
using AVBlog.Application.Extensions.Samples;
using AVBlog.Application.Services.Base;
using AVBlog.Application.ViewModels.Samples;
using AVBlog.Domain.Entities.Samples;
using AVBlog.Domain.Repositories.Samples.UserVimeoVideos;

namespace AVBlog.Application.Services.Samples.UsersVideos
{
    public class UserVideoService : BaseService<UserVimeoVideo, UserVimeoVideoViewModel>, IUserVideoService
    {
        public UserVideoService(
            IUserVimeoVideoQueryRepository queryRepository,
            IUserVimeoVideoCommandRepository commandRepository,
            IMapper mapper)
            : base(queryRepository, commandRepository, mapper)
        {

        }

        public override async Task<UserVimeoVideoViewModel?> GetByIdAsync(Guid id)
        {
            var userVimeoVideoRepository = _queryRepository as IUserVimeoVideoQueryRepository;
            var userVideoPermission = await userVimeoVideoRepository!.GetUserVideoByIdAsync(id);
            return _mapper.Map<UserVimeoVideoViewModel?>(userVideoPermission);
        }

        public override async Task<IEnumerable<UserVimeoVideoViewModel>> GetAllAsync()
        {
            var userVimeoVideoRepository = _queryRepository as IUserVimeoVideoQueryRepository;
            var userVideoPermissions = await userVimeoVideoRepository!.GetAllUserVideoAsync();
            return _mapper.Map<IEnumerable<UserVimeoVideoViewModel>>(userVideoPermissions);
        }

        public async Task<bool> IsPermissionExistAsync(string userId, Guid videoId, Guid? excludedId = default)
        {
            var userVimeoVideoRepository = _queryRepository as IUserVimeoVideoQueryRepository;
            return await userVimeoVideoRepository!.IsPermissionExistAsync(userId, videoId, excludedId);
        }

        protected override void UpdateEntityFromViewModel(UserVimeoVideoViewModel viewModel, UserVimeoVideo entity)
        {
            entity.UpdateFromModel(viewModel);
        }
    }
}
