using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DevaloreAssignment.Authentication
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAuthAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out var extrctedApiKey))
            {
                context.Result = new UnauthorizedObjectResult("Api key missing");
                return;
            }

            var configuraton = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>(); // mocking issue
            var apiKey = configuraton.GetValue<string>(AuthConstants.ApiKeySectionName);
            if (!apiKey.Equals(extrctedApiKey))
            {
                context.Result = new UnauthorizedObjectResult("Invalid API key");
                return;
            }
        }
    }
}
