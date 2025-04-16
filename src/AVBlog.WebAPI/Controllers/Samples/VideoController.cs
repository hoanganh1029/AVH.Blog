using AVBlog.Application.Commands.Samples.Videos;
using AVBlog.Application.Queries.Samples.Videos;
using AVBlog.Application.Responses;
using AVBlog.Application.ViewModels.Samples;
using AVBlog.WebAPI.Base;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AVBlog.WebAPI.Controllers.Samples
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : APIBaseController
    {
        private readonly IMediator _mediator;

        public VideoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all videos, order by published dated in desc
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetVideo()
        {
            var videos = await _mediator.Send(new GetAllWithOrderQuery { OrderBy = x => x.PublishedDate });
            return Ok(videos);
        }

        /// <summary>
        /// Get video by Id
        /// </summary>
        /// <param name="id">The video id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVideo(Guid id)
        {
            var vimeoVideo = await _mediator.Send(new GetByIdQuery { Id = id });
            if (vimeoVideo == null)
            {
                return NotFound();
            }

            return Ok(vimeoVideo);
        }

        /// <summary>
        /// Add new video
        /// </summary>
        /// <remarks>
        /// The property id can be removed
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult> PostVideo([Bind("VimeoId,Title,VideoType,PublishedDate,Description,Presenter")] VimeoVideoViewModel vimeoVideoViewModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(new AddVideoCommand { VimeoVideoViewModel = vimeoVideoViewModel });
                if (response.Success)
                {
                    var responseWithContent = response as Response<VimeoVideoViewModel>;
                    var videoViewModel = responseWithContent?.Content;
                    return CreatedAtAction(nameof(PostVideo), new { id = videoViewModel?.Id }, videoViewModel);
                }

                ModelState.AddModelError(nameof(response.Message), response.Message);
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Update an existing video
        /// </summary>
        /// <param name="id">The video id</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVideo(Guid id, [Bind("Id,VimeoId,Title,VideoType,PublishedDate,Description,Presenter")] VimeoVideoViewModel vimeoVideoViewModel)
        {
            if (id != vimeoVideoViewModel.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(new UpdateVideoCommand { Id = id, VimeoVideoViewModel = vimeoVideoViewModel });
                if (response.Success)
                {
                    return Ok();
                }

                ModelState.AddModelError(nameof(response.Message), response.Message);
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Delete video
        /// </summary>
        /// <param name="id">The video id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideo(Guid id)
        {
            var response = await _mediator.Send(new DeleteVideoCommand { Id = id });
            if (response.Success)
            {
                return Ok();
            }

            return HandleResponseAsActionResult(response);
        }
    }
}
