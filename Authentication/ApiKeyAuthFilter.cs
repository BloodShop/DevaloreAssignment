﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DevaloreAssignment.Authentication
{
    public class ApiKeyAuthFilter : IAuthorizationFilter
    {
        public readonly IConfiguration _configuration;

        public ApiKeyAuthFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out var extrctedApiKey))
            {
                context.Result = new UnauthorizedObjectResult("Api key missing");
                return;
            }

            var apiKey = _configuration.GetValue<string>(AuthConstants.ApiKeySectionName);
            if (!apiKey.Equals(extrctedApiKey))
            {
                context.Result = new UnauthorizedObjectResult("Invalid API key");
                return;
            }
        }
    }
}
