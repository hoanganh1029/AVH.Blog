using AVBlog.Application.Responses;
using AVBlog.Application.Services.Base;
using AVBlog.Application.ViewModels.Samples;
using AVBlog.Domain.Entities.Samples;
using AVBlog.Domain.LifeTime;

namespace AVBlog.Application.Services.Samples.Videos
{
    public interface IVideoService : IBaseService<VimeoVideo, VimeoVideoViewModel>, IScopedDependency
    {
        Task<IEnumerable<VimeoVideoViewModel>> GetByUserIdAsync(string userId);
        new Task<Response> AddAsync(VimeoVideoViewModel viewModel);
        new Task<Response> UpdateAsync(Guid id, VimeoVideoViewModel viewModel);
    }
}
