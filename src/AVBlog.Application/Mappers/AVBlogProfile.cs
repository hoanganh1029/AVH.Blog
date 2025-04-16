using AutoMapper;
using AVBlog.Application.Extensions;
using AVBlog.Application.ViewModels.Admin;
using AVBlog.Application.ViewModels.Samples;
using AVBlog.Domain.ProjectionModels.Samples;
using AVBlog.Domain.Entities.Samples;
using AVBlog.Domain.Entities.Users;

namespace AVBlog.Application.Mappers
{
    public class AVBlogProfile : Profile
    {
        public AVBlogProfile()
        {
            CreateMap<VimeoVideoViewModel, VimeoVideo>()
                .ForMember(dest => dest.PublishedDate, opt => opt.MapFrom(src => src.PublishedDate.ToDateTime()));
            CreateMap<VimeoVideo, VimeoVideoViewModel>()
                .ForMember(dest => dest.PublishedDate, opt => opt.MapFrom(src => src.PublishedDate.ToDateOnly()));

            CreateMap<UserVimeoVideoViewModel, UserVimeoVideo>();
            CreateMap<UserVimeoVideo, UserVimeoVideoViewModel>()
                .ForMember(dest => dest.VimeoId, opt => opt.MapFrom(src => src.VimeoVideo.VimeoId))
                .ForMember(dest => dest.VideoTitle, opt => opt.MapFrom(src => src.VimeoVideo.Title))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));

            CreateMap<UserVimeoVideoProjection, UserVimeoVideoViewModel>();

            CreateMap<UserVimeoVideo, VimeoVideoViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.VimeoVideo.Id))
                .ForMember(dest => dest.VimeoId, opt => opt.MapFrom(src => src.VimeoVideo.VimeoId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.VimeoVideo.Title))
                .ForMember(dest => dest.VideoType, opt => opt.MapFrom(src => src.VimeoVideo.VideoType))
                .ForMember(dest => dest.PublishedDate, opt => opt.MapFrom(src => src.VimeoVideo.PublishedDate.ToDateOnly()))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.VimeoVideo.Description))
                .ForMember(dest => dest.Presenter, opt => opt.MapFrom(src => src.VimeoVideo.Presenter));

            CreateMap<AppUser, UserViewModel>();
            CreateMap<UserViewModel, AppUser>();

            CreateMap<UserWithRoleProjection, UserViewModel>();

            CreateMap<UserViewModel, ResetPasswordViewModel>()
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.Email));
        }
    }
}
