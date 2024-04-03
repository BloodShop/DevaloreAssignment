using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DevaloreAssignment.Controllers
{
    public class AuthController : Controller
    {
        [HttpPost("/mvc/login")]
        public async Task<IActionResult> Login()
        {
            await HttpContext.SignInAsync("default", new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                    },"default")),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                });
            return Ok();
        }
    }
}
