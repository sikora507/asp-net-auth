using System.Security.Claims;
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
        var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
        return Ok(new { Message = "Index route in HomeController", Email = userEmail });
    }
    
    [Authorize]
    [HttpGet("/protected")]
    public IActionResult Protected()
    {
        var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
        return Ok(new { Message = "This is a protected route", Email = userEmail });
    }
}