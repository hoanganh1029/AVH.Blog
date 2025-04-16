using AutoMapper;
using AVBlog.Application.Services.Samples.UsersVideos;
using AVBlog.Application.Services.Samples.Videos;
using AVBlog.Application.ViewModels.Samples;
using AVBlog.Domain.Constants;
using AVBlog.Domain.Entities.Users;
using AVBlog.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AVBlog.WebApp.Areas.Admin.Controllers.Samples
{
    /// <summary>
    /// TODO: Use CQRS, Mediatr, remove _userVideoService
    /// </summary>
    public class UserVideoPermissionController : AdminBaseController
    {
        private readonly IUserVideoService _userVideoService;
        private readonly IVideoService _videoService;
        private readonly AVBlogCommandContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserVideoPermissionController(
            IUserVideoService userVideoService,
            AVBlogCommandContext context,
            UserManager<AppUser> userManager,
            IMapper mapper,
            IVideoService videoService)
        {
            _userVideoService = userVideoService;
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _videoService = videoService;
        }

        // GET: Admin/UserVimeoVideos
        public async Task<IActionResult> Index()
        {
            var userVideoPermissions = await _userVideoService.GetAllAsync();

            // Have the same result but lazy loading increases the number of queries to the database
            //var userVideoPermissions = await _userVideoService.GetAllWithOrderAsync(x => x.User.UserName, OrderType.Ascending);

            return View(userVideoPermissions);
        }

        // GET: Admin/UserVimeoVideos/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var userVimeoVideo = await _userVideoService.GetByIdAsync(id);
            if (userVimeoVideo == null)
            {
                return NotFound();
            }

            return View(userVimeoVideo);
        }

        // GET: Admin/UserVimeoVideos/Create
        public async Task<IActionResult> Create()
        {
            await SetUserVideoViewData();
            return View();
        }

        // POST: Admin/UserVimeoVideos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,VimeoVideoId,ExpiredDate")] UserVimeoVideoViewModel userVimeoVideoViewModel)
        {
            if (ModelState.IsValid)
            {
                var isDuplicate = await _userVideoService.IsPermissionExistAsync(userVimeoVideoViewModel.UserId, userVimeoVideoViewModel.VimeoVideoId);
                if (isDuplicate)
                {
                    ModelState.AddModelError(string.Empty, "User has already had this video.");
                    return await GetCurrentView();
                }

                await _userVideoService.AddAsync(userVimeoVideoViewModel);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty,
                string.Join("\n",
                ModelState.Where(x => x.Value?.ValidationState == ModelValidationState.Invalid)
                          .Select(x => x.Value?.Errors.Select(x => x.ErrorMessage))));

                return await GetCurrentView();
            }

            async Task<ViewResult> GetCurrentView()
            {
                await SetUserVideoViewData();
                return View(userVimeoVideoViewModel);
            }
        }

        // GET: Admin/UserVimeoVideos/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var userVimeoVideo = await _userVideoService.GetByIdAsync(id);
            if (userVimeoVideo == null)
            {
                return NotFound();
            }

            await SetUserVideoViewData();
            return View(userVimeoVideo);
        }

        // POST: Admin/UserVimeoVideos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserId,VimeoVideoId,ExpiredDate")] UserVimeoVideoViewModel userVimeoVideoViewModel)
        {
            if (id != userVimeoVideoViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var isExist = await _userVideoService.IsExistAsync(id);
                if (!isExist)
                {
                    return NotFound();
                }

                var isDuplicate = await _userVideoService.IsPermissionExistAsync(userVimeoVideoViewModel.UserId, userVimeoVideoViewModel.VimeoVideoId, id);
                if (isDuplicate)
                {
                    ModelState.AddModelError(string.Empty, "User has already had this video.");
                    return await GetCurrentView();
                }

                await _userVideoService.UpdateAsync(id, userVimeoVideoViewModel);

                return RedirectToAction(nameof(Index));
            }

            return await GetCurrentView();

            async Task<ViewResult> GetCurrentView()
            {
                await SetUserVideoViewData();
                return View(userVimeoVideoViewModel);
            }
        }

        // GET: Admin/UserVimeoVideos/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var userVimeoVideo = await _userVideoService.GetByIdAsync(id);
            if (userVimeoVideo == null)
            {
                return NotFound();
            }

            return View(userVimeoVideo);
        }

        // POST: Admin/UserVimeoVideos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var isExist = await _userVideoService.IsExistAsync(id);
            if (!isExist)
            {
                return NotFound();
            }

            await _userVideoService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task SetUserVideoViewData()
        {
            var normalUsers = await _userManager.GetUsersInRoleAsync(RoleConstant.User);
            ViewData["Users"] = new SelectList(normalUsers, "Id", "UserName");

            var videos = await _videoService.GetAllAsync();
            ViewData["Videos"] = new SelectList(videos, "Id", "Title");
        }
    }
}
