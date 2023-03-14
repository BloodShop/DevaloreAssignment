using AutoMapper;
using DevaloreAssignment.Dto;
using DevaloreAssignment.EndpointsDefinition.SecretLibs;
using DevaloreAssignment.Models;
using DevaloreAssignment.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevaloreAssignment.EndpointsDefinition
{
    public class UserEndpointDefinition : /*CarterModule,*/ IEndpointDefinition
    {
        public void DefineEndpoints(WebApplication app)
        {
            app.MapGet("/last-user", GetNewUser);
            app.MapPost("/create-user", CreateNewUser);
            app.MapPut("/users/{email}", UpdateUserData)
                /*.AddEndpointFilter(async (ctx, next) => await Validate(ctx, next))
                .Accepts<User>("application/json")
                .Produces<User>(statusCode: 200, contentType: "application/json")*/;
        }

        /*static async ValueTask<object> Validate(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var validator = (IValidator<User>)context?.Arguments[1];
            var user = (User)context?.Arguments[2];

            var result = await validator.ValidateAsync(user);
            if (!result.IsValid)
            {
                var errors = string.Join(' ', result.Errors);
                return Results.Problem(errors);
            }
            return await next(context);
        }*/

        public void DefineServices(IServiceCollection services) => services.AddSingleton<IUserService, UserService>();

        internal IResult UpdateUserData([FromServices] IUserService userService, [FromQuery] string email, User updatedUser)
        {
            var user = userService.GetUserByEmail(email);
            if (user is null)
            {
                return Results.NotFound();
            }
            userService.UpdateUserData(updatedUser);
            return Results.Ok(updatedUser);
        }

        internal async Task<IResult> CreateNewUser([FromServices] IUserService userService, IMapper mapper, UserDto user)
        {
            var userToCreate = mapper.Map<User>(user);

            return await userService.CreateNewUser(userToCreate)
                ? Results.Created($"/users/{userToCreate.id.name + userToCreate.id.value}", userToCreate)
                : Results.Problem();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Standard, Administrator")]
        internal IResult GetNewUser([FromServices] IUserService userService)
        {
            return null;
        }
    }
}
