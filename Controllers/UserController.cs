using AutoMapper;
using DevaloreAssignment.Dto;
using Microsoft.AspNetCore.Mvc;
using WeatherApiHttp.Clients;

namespace DevaloreAssignment.Controllers
{
    [ApiController]
    [Route("[contoller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        public readonly IUserClient _userService;

        public UserController(ILogger<UserController> logger, IMapper mapper, IUserClient userClient)
        {
            _logger = logger;
            _mapper = mapper;
            _userService = userClient;
        }

        [HttpGet("/users-by-gender/{gender}")]
        public async Task<IActionResult> GetUsersData(/*[FromQuery]*/ string gender)
        {
            var users = await _userService.GetUsersData(gender);
            _logger.LogInformation("users retrieved");
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
            ErrorOr<string> getMostPopularCountry = _userService.GetMostPopularCountry();

            return getMostPopularCountry.Match(
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

        [HttpPost("/post-user")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateNewUser([FromBody] CreateUserDto userDto)
        {
            if (userDto == null)
                return BadRequest(ModelState);
            
            var user = _userService.GetNewUser();

            if (user != null)
            {
                ModelState.AddModelError("", "user already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //var userMap = _mapper.Map<UserDto>(userDto);

            /*if (!_userService.CreateNewUser(userDto))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }*/

            return Ok("Successfully created");
        }

    }
}
