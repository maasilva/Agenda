using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Agenda.Filters
{
    public class AuthFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var user = context.HttpContext.User;
            if (!user.Identity.IsAuthenticated && !context.ActionDescriptor.DisplayName.Contains("Login"))
            {
                context.Result = new RedirectToPageResult("/Login");
            }
        }
        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
