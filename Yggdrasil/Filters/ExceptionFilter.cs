using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Yggdrasil.Models.YggdrasilApi;

namespace Yggdrasil.Filters {
    public class ExceptionFilter : IAsyncExceptionFilter {
        public async Task OnExceptionAsync(ExceptionContext context) {
            if (context.ExceptionHandled) return;
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<ExceptionFilter>>();

            context.Result = new JsonResult(new ExceptionResponse {
                Error = nameof(context.Exception),
                ErrorMessage = context.Exception.Message,
                Cause = context.Exception.StackTrace
            });

            logger.LogError(context.Exception, "{0} 访问 {1} 时发生错误", context.HttpContext.Connection.RemoteIpAddress, context.HttpContext.Request.Path);

            await Task.CompletedTask;
        }
    }
}
