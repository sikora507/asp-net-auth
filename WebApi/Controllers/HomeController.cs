using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[AllowAnonymous]
[ApiController]
[Route("/")]
public class HomeController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok(new { Message = "Index route in HomeController" });
    }
    
    [HttpGet("/protected")]
    [Authorize]
    public IActionResult Protected()
    {
        return Ok(new { Message = "This is a protected route" });
    }
}