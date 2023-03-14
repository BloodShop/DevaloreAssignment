namespace DevaloreAssignment.EndpointsDefinition.SecretLibs
{
    public interface IEndpointDefinition
    {
        void DefineServices(IServiceCollection services);
        void DefineEndpoints(WebApplication app);
    }
}