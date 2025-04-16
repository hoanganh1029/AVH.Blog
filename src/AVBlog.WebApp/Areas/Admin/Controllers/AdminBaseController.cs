using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AVBlog.Domain.Constants;
using AVBlog.WebApp.Filters;
using AVBlog.WebApp.Base;
namespace AVBlog.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleConstant.Admin)]
    [LogAction(Order = int.MinValue)]
    public class AdminBaseController : BaseController
    {
    }
}
