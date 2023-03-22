using AutoMapper;
using DevaloreAssignment.Dto;
using Microsoft.AspNetCore.Mvc;
using DevaloreAssignment.Services;
using DevaloreAssignment.Models;

namespace DevaloreAssignment.Controllers
{
    [ApiController]
    [Route("[contoller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IMapper mapper, IUserService userClient)
        {
            _logger = logger;
            _mapper = mapper;
            _userService = userClient;
        }

        [HttpGet("/users-by-gender/{gender:sex}")]
        public async Task<IActionResult> GetUsersData(/*[FromQuery]*/ string gender)
        {
            var users = await _userService.GetUsersData(gender);
            _logger.LogInformation("users retrieved"); // TODO: use serilog 
            return Ok(users);
        }

        [HttpGet("/popular-country")]
        public async Task<IActionResult> GetMostPopularCountry()
        {
            var country = await _userService.GetMostPopularCountry();
            return country == null ? NotFound() : Ok(country);
        }

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
        
        public async Task<IActionResult> CreateNewUser([FromBody] CreateUserDto userDto)
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

            var userMap = _mapper.Map<User>(userDto);

            if (!await _userService.CreateNewUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

    }
}
