using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;
using Discite.API.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Discite.API.Attributes
{
    public class AuthorizeAdminAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.Request.uid() == 0)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
