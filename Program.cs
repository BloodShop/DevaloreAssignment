using DevaloreAssignment.EndpointsDefinition.SecretLibs;
using DevaloreAssignment.Models;
using DevaloreAssignment.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddEndpointDefinitions(typeof(ResultResponse));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpClient("usersapi", client => client.BaseAddress = new Uri(builder.Configuration["BaseAddress"]));

var app = builder.Build();

app.UseEndpointDefinitions();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

//app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.Run();
