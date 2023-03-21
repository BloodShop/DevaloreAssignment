using DevaloreAssignment.EndpointsDefinition.SecretLibs;
using DevaloreAssignment.Models;
using DevaloreAssignment.Services;
using Microsoft.Net.Http.Headers;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("https://cdpn.io")
                //.AllowAnyHeader()
                .WithHeaders(/*HeaderNames.ContentType, */"application/x-www-form-urlencoded", "application/json")
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

builder.Services.AddControllers();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddEndpointDefinitions(typeof(ResultResponse));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpClient("usersapi", client => client.BaseAddress = new Uri(builder.Configuration["BaseAddress"]));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseEndpointDefinitions();
//app.UseStaticFiles();
//app.UseRouting();

app.UseCors();

app.UseAuthorization();
app.MapControllers();

//app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.Run();
