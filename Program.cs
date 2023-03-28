using DevaloreAssignment.AppSettingsOptions;
using DevaloreAssignment.Authentication;
using DevaloreAssignment.EndpointsDefinition.SecretLibs;
using DevaloreAssignment.Middlewares;
using DevaloreAssignment.Models;
using DevaloreAssignment.RoutingConstraints;
using DevaloreAssignment.Services;
using Polly;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureAppConfiguration((context, bldr) =>
{
    var inmemory = new Dictionary<string, string>
    {
        { "UserApiOptions:Name", "apiuser"}
    };
    bldr.AddJsonFile("MyConfig.json", optional: false);
    bldr.AddInMemoryCollection(inmemory); // The last assignment to the "UserApiOptions:Name" will ovveride the others
});

var corsOptions = builder.Configuration.GetSection("Cors").Get<MyCorsOptions>();
var userApiOptions = builder.Configuration.GetSection("UserApi").Get<UserApiOptions>();

builder.Services.Configure<UserApiOptions>(builder.Configuration.GetSection("UserApi"));
//builder.Services.AddOptions<UserApiOptions>()
//    .Bind(builder.Configuration.GetSection(UserApiOptions.UserApi))
//    .ValidateDataAnnotations();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy
                .WithOrigins(corsOptions.AllowedOrigins)
                .AllowAnyHeader()
                //.WithHeaders(HeaderNames.ContentType, "Content-Type")
                .AllowAnyMethod();
        });
});
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddEndpointDefinitions(typeof(ResultResponse));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var timeout = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(5));
builder.Services
    .AddHttpClient(userApiOptions.Name, client => client.BaseAddress = new Uri(userApiOptions.BaseAddress))
    .AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(2)))
    .AddTransientHttpErrorPolicy(policy => policy.CircuitBreakerAsync(5, TimeSpan.FromSeconds(5)))
    .AddPolicyHandler(request => 
        request.Method == HttpMethod.Get
            ? timeout
            : Policy.NoOpAsync<HttpResponseMessage>());

//builder.Services.AddHttpClient<IUserService, UserService>(client => client.BaseAddress = new Uri(builder.Configuration["BaseAddress"])); // Inject HttpClient at the service
builder.Services.AddControllers(/*x => x.Filters.Add<ApiKeyAuthFilter>()*/);
//.AddXmlSerializerFormatters()
//.AddNewtonsoftJson()
//.AddJsonOptions(jsonOptions =>
//{
//    jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase; // null
//    //jsonOptions.JsonSerializerOptions.Converters.Add();
//});

builder.Services.AddScoped<ApiKeyAuthFilter>();

builder.Services.AddRouting(options => //Custom Constraint check routing match
{
    options.ConstraintMap.Add("sex", typeof(GenderConstraint));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.Use(async (context, next) =>
    {
        var apiuser = builder.Configuration["UserApiOptions:Name"];
        //Console.WriteLine("!!!! ", apiuser);

        await next();
    });

    app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
    app.UseEndpointDefinitions();
}

app.UseHttpsRedirection();
//app.UseStaticFiles();
//app.UseRouting();

app.UseCors();

//app.UseMiddleware<ApiKeyAuthMiddleware>();

app.UseAuthorization();
app.MapControllers();

app.Run();
