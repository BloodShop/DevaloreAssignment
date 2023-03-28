namespace DevaloreAssignment.Authentication
{
    public sealed class UnauthorizedHttpObjectResult // : IResult, IStatusCodeHttpResult
    {
        public Task ExecuteAsync(HttpContext httpContext)
        {
            throw new NotImplementedException();
        }
    }
}
