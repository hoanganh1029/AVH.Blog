using Microsoft.AspNetCore.Mvc.Filters;

namespace AVBlog.WebApp.Filters;

public class LogActionAttribute : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var userName = context.HttpContext.User.Identity?.Name;        
        var path = context.HttpContext.Request.Path;
        var method = context.HttpContext.Request.Method;
        var area = context.RouteData.Values["area"]?.ToString();
        //return base.OnActionExecutionAsync(context, next);
        Console.WriteLine($"[{DateTime.UtcNow.ToString("dd/MM/yyyy hh:mm:ss")}] Begin: User {userName} execute {method}, path {path}, area {area}");
        await next();
        Console.WriteLine($"[{DateTime.UtcNow.ToString("dd/MM/yyyy hh:mm:ss")}] End: User {userName} execute {method}, path {path}, area {area}");
    }
}
