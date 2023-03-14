using DevaloreAssignment.EndpointsDefinition.SecretLibs;
using Microsoft.OpenApi.Models;

namespace DevaloreAssignment.EndpointsDefinition
{
    public class SwaggerEndpointDefinition : IEndpointDefinition
    {
        public void DefineEndpoints(WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(C => C.SwaggerEndpoint("/swagger/v1/swagger.json", "SimpleApi v1"));
        }

        public void DefineServices(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DevaloreApi", Version = "v1" });
            });
        }
    }
}
