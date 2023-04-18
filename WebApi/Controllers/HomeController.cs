using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("/")]
public class HomeController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var userEmail = HttpContext.User.FindFirstValue(ClaimTypes.Email);

        return Ok(new { Name = "Google Auth Example", Version = "1.0.0", email = userEmail });
    }
}