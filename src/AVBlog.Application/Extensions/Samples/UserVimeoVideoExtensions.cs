using AVBlog.Application.ViewModels.Samples;
using AVBlog.Domain.Entities.Samples;

namespace AVBlog.Application.Extensions.Samples
{
    public static class UserVimeoVideoExtensions
    {
        public static void UpdateFromModel(this UserVimeoVideo userVimeoVideo, UserVimeoVideoViewModel viewModel)
        {
            userVimeoVideo.VimeoVideoId = viewModel.VimeoVideoId;
            userVimeoVideo.UserId = viewModel.UserId;
            userVimeoVideo.ExpiredDate = viewModel.ExpiredDate;
        }
    }
}
