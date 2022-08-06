using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using System.Text.Json;
using Yggdrasil.Models.YggdrasilApi;
using Yggdrasil.Service;

namespace Yggdrasil.Filters {
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class AuthorizationFilter : Attribute, IAsyncAuthorizationFilter {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context) {
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var controllerType = actionDescriptor!.ControllerTypeInfo;
            var methodType = actionDescriptor.MethodInfo;
            var allowAnonymouse = context.Filters.Any(u => u is IAllowAnonymousFilter)
                || controllerType.IsDefined(typeof(AllowAnonymousAttribute), true)
                || methodType.IsDefined(typeof(AllowAnonymousAttribute), true);
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<AuthorizationFilter>>();

            if (!allowAnonymouse) {
                context.HttpContext.Request.EnableBuffering();
                using (StreamReader reader = new StreamReader(context.HttpContext.Request.Body, leaveOpen: true)) {
                    try {
                        if (JsonSerializer.Deserialize<TokenRequestResponse>(await reader.ReadToEndAsync()) is TokenRequestResponse request) {
                            var yggdrasilService = context.HttpContext.RequestServices.GetRequiredService<YggdrasilService>();
                            try {
                                context.HttpContext.User = yggdrasilService.GetTokenInfo(request.AccessToken);

                            } catch (Exception ex) {
                                var result = new JsonResult(new ExceptionResponse {
                                    Error = "ForbiddenOperationException",
                                    ErrorMessage = "Invalid token.",
                                    Cause = null
                                });

                                result.StatusCode = 401;
                                context.Result = result;
                                logger.LogError("拒绝 {0} {1} {2}", context.HttpContext.Connection.RemoteIpAddress, context.HttpContext.Request.Method, context.HttpContext.Request.Path);
                            }
                        } else {
                            var result = new JsonResult(new ExceptionResponse {
                                Error = "ForbiddenOperationException",
                                ErrorMessage = "Invalid token.",
                                Cause = null
                            });

                            result.StatusCode = 401;
                            context.Result = result;
                            logger.LogError("拒绝 {0} {1} {2}", context.HttpContext.Connection.RemoteIpAddress, context.HttpContext.Request.Method, context.HttpContext.Request.Path);
                        }
                    } catch (Exception ex) {
                        var result = new JsonResult(new ExceptionResponse {
                            Error = "ForbiddenOperationException",
                            ErrorMessage = "Invalid token.",
                            Cause = ex.ToString()
                        });

                        result.StatusCode = 401;
                        context.Result = result;
                        logger.LogError("拒绝 {0} {1} {2}", context.HttpContext.Connection.RemoteIpAddress, context.HttpContext.Request.Method, context.HttpContext.Request.Path);
                    }
                }

                context.HttpContext.Request.Body.Seek(0, SeekOrigin.Begin);
            }

            await Task.CompletedTask;
        }
    }
}
