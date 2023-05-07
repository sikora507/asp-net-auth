using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("/")]
public class HomeController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok(new { Message = "Index route in HomeController" });
    }
    
    [Authorize]
    [HttpGet("/protected")]
    public IActionResult Protected()
    {
        return Ok(new { Message = "This is a protected route" });
    }
}