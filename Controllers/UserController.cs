using AutoMapper;
using DevaloreAssignment.Authentication;
using DevaloreAssignment.Dto;
using DevaloreAssignment.Models;
using DevaloreAssignment.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Web.Http.ModelBinding;

#nullable enable
namespace DevaloreAssignment.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

        [HttpGet("by-gender/{gender:sex}")]
        [ServiceFilter(typeof(ApiKeyAuthFilter))] // better approach
        /*[ApiKeyAuth]*/
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<User>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ModelStateDictionary))]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersData([FromRoute] string gender)
        {
            var users = await _userService.GetUsersData(gender);

            if (users is null)
                ModelState.AddModelError(nameof(gender), "Users do not exict");

            if (!ModelState.IsValid)
                return NotFound(ModelState);

            _logger.LogInformation("users retrieved"); // TODO: use serilog 
            return new JsonResult(users, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }

        [HttpGet("popular-country")]
        [ServiceFilter(typeof(ApiKeyAuthFilter))] // better approach
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMostPopularCountry()
        {
            var country = await _userService.GetMostPopularCountry();
            return country == null ? NotFound() : Ok(country);
        }

        [HttpGet("list-of-mails/{format?}")]
        [FormatFilter]
        [Produces("application/json")]
        public async Task<IActionResult> GetListOfMails()
        {
            var mails = await _userService.GetListOfMails();
            return Ok(mails);
        }

        [HttpGet("oldest-user")]
        public async Task<IActionResult> GetOldestUser()
        {
            var users = await _userService.GetOldestUser();
            return Ok(users);
        }

        [HttpPost("post-user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CreateNewUser([FromBody] CreateUserDto userDto)
        {
            if (userDto == null)
                return BadRequest(ModelState);

            var user = _userService.GetNewUser();

            if (user != null)
            {
                ModelState.AddModelError("", "user already exists");
                return UnprocessableEntity(ModelState);
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
