using Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PmsAPI.Filters
{
    public class CustomTokenAuthFilterAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public string TokenHeader = "TokenHeader";
        public string UserId = "UserId";

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(TokenHeader, out var tokenString))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            int userId = 0;

            if (context.HttpContext.Request.Headers.TryGetValue(UserId,out var userIdString))
            {     
                int.TryParse(userIdString, out userId);
            }
            else
            {
                string errorMessage = $"{nameof(userId)} not specified";
                context.Result = new BadRequestObjectResult(new { errorMessage });
                return;
            }

            var tokenManager = context.HttpContext.RequestServices.GetService(typeof(ICustomTokenManager)) as ICustomTokenManager;

            if (tokenManager != null && userId != 0)
            {
                var validToken = await tokenManager.VerifyTokenAsync(tokenString, userId);

                if (!validToken)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

            }
            else
            {
                return;
            }

        }
    }
}
