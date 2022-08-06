using Microsoft.AspNetCore.Mvc.Filters;

namespace Yggdrasil.Filters {
    public class ActionFilter : IAsyncActionFilter {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<ActionFilter>>();
            logger.LogInformation("{0} {1} {2}", context.HttpContext.Connection.RemoteIpAddress, context.HttpContext.Request.Method, context.HttpContext.Request.Path);

            await next();
        }
    }
}
