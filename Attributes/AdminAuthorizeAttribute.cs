using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using library_backend.Services;
using library_backend.Entities;

namespace library_backend.Attributes
{
    public class AdminAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var userservice = context.HttpContext
                                    .RequestServices
                                    .GetService(typeof(IUserService))
                                    as IUserService;
            var id = context.HttpContext.Session.GetString("UserID");
            if (
                id == null ||
                userservice.GetUserById(id) == null ||
                userservice.GetUserById(id).type != user.UserType.管理员
                )
            {
                context.Result = new StatusCodeResult(403);
            }
        }
    }
}