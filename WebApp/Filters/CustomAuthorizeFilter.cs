using BusinessLayer.Interfaces.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Web.Http;

namespace WebApp.Filters
{
    public class CustomAuthorizeFilter : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization (AuthorizationFilterContext context)
        {
            var IsAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;

            if (IsAuthenticated)
            {
                var _verifyToken = context.HttpContext.RequestServices.GetService<IVerifyToken>();
                _verifyToken.Verify(context);
            }
            else
            {
                context.HttpContext.Session.Clear();
                context.Result = new RedirectResult("~/Login/LogUser");
            }
        }
    }
}
