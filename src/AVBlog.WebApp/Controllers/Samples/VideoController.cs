using AVBlog.Application.Queries.Samples.Videos;
using AVBlog.Application.Services.Samples.Videos;
using AVBlog.Domain.Constants;
using AVBlog.Domain.Entities.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AVBlog.WebApp.Controllers.Samples
{
    [Authorize(Roles = $"{RoleConstant.User},{RoleConstant.Admin}")]
    public class VideoController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMediator _mediator;

        public VideoController(
            UserManager<AppUser> userManager,
            IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        // GET: VimeoVideos
        public async Task<IActionResult> Index()
        {
            var viewName = "~/Views/Samples/Video/Index.cshtml";
            if (User.IsInRole(RoleConstant.Admin))
            {
                //var videoViewModels = await _mediator.Send(new GetAllWithOrderQuery{OrderBy = x => x.PublishedDate});
                var videoViewModels = await _mediator.Send(new GetAllQuery());
                return View(viewName, videoViewModels);
            }
            else
            {
                var currentUserId = _userManager.GetUserId(User);
                if (currentUserId is null)
                {
                    return BadRequest();
                }

                var videoViewModels = await _mediator.Send(new GetByUserIdQuery { UserId = currentUserId });
                return View(viewName, videoViewModels);
            }
        }
    }
}
