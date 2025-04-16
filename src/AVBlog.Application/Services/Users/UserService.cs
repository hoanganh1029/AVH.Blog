using AutoMapper;
using AVBlog.Application.Extensions;
using AVBlog.Application.Responses;
using AVBlog.Application.ViewModels.Admin;
using AVBlog.Domain.Constants;
using AVBlog.Domain.Entities.Users;
using AVBlog.Domain.Repositories.Users;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AVBlog.Application.Services.Users
{
    public class UserService : GeneralServiceResponseAttributes, IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(UserManager<AppUser> userManager,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserViewModel>> GetUsersAsync(ClaimsPrincipal user)
        {
            var isSuperAdmin = user.IsSuperAdmin();
            var currentUserId = _userManager.GetUserId(user);
            var users = await _userRepository.GetByRoleAsync(isSuperAdmin ? string.Empty : RoleConstant.User, currentUserId!);
            return _mapper.Map<IEnumerable<UserViewModel>>(users);
        }

        public async Task<Response> GetByUserIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Success(_mapper.Map<UserViewModel>(user));
        }

        public async Task<Response> CreateUserAsync(UserViewModel userModel)
        {
            var appUser = _mapper.Map<AppUser>(userModel);
            appUser.Id = Guid.NewGuid().ToString();
            appUser.UserName = userModel.Email;
            appUser.EmailConfirmed = true;

            var result = await _userManager.CreateAsync(appUser, userModel.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(appUser, userModel.Role);
                return Success();
            }
            return BadRequest(result.GetErrorsMessage());
        }

        public async Task<Response> UpdateUserAsync(UserViewModel userModel)
        {
            var user = await _userManager.FindByIdAsync(userModel.Id!);

            if (user == null)
            {
                return NotFound();
            }

            user.AllowDownload = userModel.AllowDownload;
            user.ExpiredDate = userModel.ExpiredDate;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded ? Success() : BadRequest(result.GetErrorsMessage());
        }

        public async Task<Response> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded ? Success() : BadRequest(result.GetErrorsMessage());
        }

        public async Task<Response> ResetPasswordAsync(ResetPasswordViewModel resetPasswordModel)
        {
            var user = await _userManager.FindByIdAsync(resetPasswordModel.Id);
            if (user == null)
            {
                return NotFound();
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, token, resetPasswordModel.NewPassword);
            return result.Succeeded ? Success() : BadRequest(result.GetErrorsMessage());
        }
    }
}
