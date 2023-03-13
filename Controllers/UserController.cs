using Microsoft.AspNetCore.Mvc;
using WeatherApiHttp.Clients;

namespace DevaloreAssignment.Controllers
{
    [ApiController]
    [Route("[contoller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        public readonly IUserClient _userService;

        public UserController(ILogger<UserController> logger, IUserClient userClient)
        {
            _logger = logger;
            _userService = userClient;
        }

        [HttpGet("/users-by-gender/{gender}")]
        public async Task<IActionResult> GetUsersData(/*[FromQuery]*/ string gender)
        {
            var users = await _userService.GetUsersData(gender);
            return Ok(users);
        }

        [HttpGet("/popular-country")]
        public async Task<IActionResult> GetMostPopularCountry()
        {
            var country = await _userService.GetMostPopularCountry();
            return country == null ? NotFound() : Ok(country);
        }

        /*[HttpGet("/popular-country")]
        public async Task<IActionResult> GetMostPopularCountry()
        {
            ErrorOr<string> getMostPoppularCountry = _userService.GetMostPopularCountry();

            return getMostPoppularCountry.Match(
                country => Ok(MapCountryResponse(country)),
                errors => Problem(errors));
        }*/

        [HttpGet("/list-of-mails")]
        public async Task<IActionResult> GetListOfMails()
        {
            var mails = await _userService.GetListOfMails();
            return Ok(mails);
        }

        [HttpGet("/oldest-user")]
        public async Task<IActionResult> GetOldestUser()
        {
            var users = await _userService.GetOldestUser();
            return Ok(users);
        }

    }
}
