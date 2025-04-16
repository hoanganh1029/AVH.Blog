using AVBlog.Application.ViewModels.Samples;
using AVBlog.Domain.Entities.Samples;

namespace AVBlog.Application.Extensions.Samples
{
    public static class VimeoVideoExtensions
    {
        public static void UpdateFromModel(this VimeoVideo vimeoVideo, VimeoVideoViewModel model)
        {
            vimeoVideo.VimeoId = model.VimeoId;
            vimeoVideo.Title = model.Title;
            vimeoVideo.VideoType = model.VideoType;
            vimeoVideo.PublishedDate = model.PublishedDate.ToDateTime();
            vimeoVideo.Description = model.Description;
            vimeoVideo.Presenter = model.Presenter;
        }
    }
}
