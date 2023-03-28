using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DevaloreAssignment.Authentication;

public class ApiKeyEndpointFilter : IEndpointFilter
{
    public readonly IConfiguration _configuration;

    public ApiKeyEndpointFilter(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    //public async ValueTask<object> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    //{
    //    if (!context.HttpContext.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out var extrctedApiKey))
    //    {
    //        return TypedResults.Unauthorized();
    //    }

    //    var apiKey = _configuration.GetValue<string>(AuthConstants.ApiKeySectionName);
    //    if (!apiKey.Equals(extrctedApiKey))
    //    {
    //        return TypedResults.Unauthorized();
    //    }

    //    return await next(context);
    //}
}
