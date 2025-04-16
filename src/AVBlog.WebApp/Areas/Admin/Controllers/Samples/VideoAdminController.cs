using AVBlog.Application.Commands.Samples.Videos;
using AVBlog.Application.Queries.Samples.Videos;
using AVBlog.Application.ViewModels.Samples;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AVBlog.WebApp.Areas.Admin.Controllers.Samples
{
    public class VideoAdminController : AdminBaseController
    {
        private readonly IMediator _mediator;

        public VideoAdminController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: Admin/Video
        public async Task<IActionResult> Index()
        {
            var videos = await _mediator.Send(new GetAllWithOrderQuery { OrderBy = x => x.PublishedDate });
            return View(videos);
        }

        // GET: Admin/Video/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vimeoVideo = await _mediator.Send(new GetByIdQuery { Id = id.Value });
            if (vimeoVideo == null)
            {
                return NotFound();
            }

            return View(vimeoVideo);
        }

        // GET: Admin/Video/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Video/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VimeoId,Title,VideoType,PublishedDate,Description,Presenter")] VimeoVideoViewModel vimeoVideoViewModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(new AddVideoCommand { VimeoVideoViewModel = vimeoVideoViewModel });
                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, response.Message);
            }
            return View(vimeoVideoViewModel);
        }

        // GET: Admin/Video/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var vimeoVideo = await _mediator.Send(new GetByIdQuery { Id = id });
            if (vimeoVideo == null)
            {
                return NotFound();
            }
            return View(vimeoVideo);
        }

        // POST: Admin/Video/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("VimeoId,Title,VideoType,PublishedDate,Description,Presenter,Id")] VimeoVideoViewModel vimeoVideoViewModel)
        {
            if (id != vimeoVideoViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(new UpdateVideoCommand { Id = id, VimeoVideoViewModel = vimeoVideoViewModel });
                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, response.Message);
            }
            return View(vimeoVideoViewModel);
        }

        // GET: Admin/Video/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var vimeoVideo = await _mediator.Send(new GetByIdQuery { Id = id });
            if (vimeoVideo == null)
            {
                return NotFound();
            }

            return View(vimeoVideo);
        }

        // POST: Admin/Video/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var response = await _mediator.Send(new DeleteVideoCommand { Id = id });
            if (response.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            return HandleResponseAsActionResult(response);
        }
    }
}
