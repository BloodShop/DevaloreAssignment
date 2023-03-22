using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DevaloreAssignment.Controllers;

[EnableCors("_myAllowSpecificOrigins")]
[Route("api/[controller]")]
[ApiController]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    //[HttpPost]
    //[DisableCors]
    //[Consumes("application/json")]
    //public IActionResult PostJson([FromBody] IEnumerable<int> values) =>
    //    Ok(new { Consumes = "application/json", Values = values });

    //[HttpPost]
    //[Consumes("application/x-www-form-urlencoded", "application/json")]
    //public IActionResult PostForm([FromForm] IEnumerable<int> values, [FromHeader(Name = "content-type")] string contentType) =>
    //    Ok(new { Consumes = contentType, Values = values });

    [HttpPost]
    //[Consumes("application/x-www-form-urlencoded", "application/json")]
    public IActionResult PostForm([FromQuery] MyComplexType myComplexType) =>
        Ok(new { Consumes = myComplexType.ContentType, Values = myComplexType.Values });
}

public class MyComplexType
{
    [FromForm]
    public IEnumerable<int>? Values { get; set; } 

    [FromHeader(Name = "content-type")]
    public string ContentType { get; set; }

    public Dictionary<string, int>? Marks { get; set; }
}