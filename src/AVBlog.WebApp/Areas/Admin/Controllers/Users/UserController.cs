using AVBlog.Application.Responses;
using AVBlog.Application.Services.Users;
using AVBlog.Application.ViewModels.Admin;
using Microsoft.AspNetCore.Mvc;

namespace AVBlog.WebApp.Areas.Admin.Controllers.Users
{
    /// <summary>
    /// TODO: Use CQRS, Mediatr, remove _userService
    /// </summary>
    public class UserController : AdminBaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: UserController
        public async Task<ActionResult> Index()
        {
            var users = await _userService.GetUsersAsync(User);
            return View(users);
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Email, Password, ExpiredDate, AllowDownload")] UserViewModel userModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _userService.CreateUserAsync(userModel);
                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, response.Message);
            }

            return View(userModel);
        }

        // GET: UserController/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var response = await _userService.GetByUserIdAsync(id) as Response<UserViewModel>;
            return HandleResponseAsActionResult(response);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id, ExpiredDate, AllowDownload")] UserViewModel userModel)
        {
            if (userModel.Id != id)
            {
                return BadRequest();
            }

            var response = await _userService.UpdateUserAsync(userModel);
            if (response.Success)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, response.Message);
            return View(userModel);
        }

        // GET: UserController/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            var response = await _userService.GetByUserIdAsync(id) as Response<UserViewModel>;
            return HandleResponseAsActionResult(response);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            var response = await _userService.DeleteUserAsync(id);
            if (response.Success)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, response.Message);
            return View(id);
        }


        // GET: UserController/ResetPassword
        public async Task<ActionResult> ResetPassword(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            if (await _userService.GetByUserIdAsync(id) is not Response<UserViewModel> response || !response.Success)
            {
                return NotFound();
            }
            return View(new ResetPasswordViewModel
            {
                Id = id,
                UserEmail = response.Content?.Email!
            });
        }

        [HttpPost]
        [ActionName("ResetPassword")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPasswordConfirmed(string id, ResetPasswordViewModel resetPasswordViewModel)
        {
            if (string.IsNullOrEmpty(id) || resetPasswordViewModel.Id != id)
            {
                return BadRequest();
            }

            var response = await _userService.ResetPasswordAsync(resetPasswordViewModel);
            if (response.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, response.Message);
                return View(resetPasswordViewModel);
            }

        }
    }
}
