using WeatherApiHttp.Clients;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IUserClient, UserClient>();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpClient("usersapi", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["BaseAddress"]);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.MapPost("/", (CreateUserDto) => "Hello world!")

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
//app.MapCarter();

app.Run();
