namespace DevaloreAssignment.Authentication
{
    public class ApiKeyAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public ApiKeyAuthMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out var extrctedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api key missing");
                return;
            }

            var apiKey = _configuration.GetValue<string>(AuthConstants.ApiKeySectionName);
            if (!apiKey.Equals(extrctedApiKey))
            {
                await context.Response.WriteAsync("Invalid API kEY");
                return;
            }

            await _next(context);
        }
    }
}
