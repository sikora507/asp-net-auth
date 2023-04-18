using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;


[AllowAnonymous]
[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    [HttpGet("signin")]
    public IActionResult SignIn()
    {
        return Challenge(
            new AuthenticationProperties { RedirectUri = "/auth/signed-in-google" },
            GoogleDefaults.AuthenticationScheme
        );
    }

    [HttpGet("signout")]
    public IActionResult SignOut()
    {
        return SignOut(
            new AuthenticationProperties { RedirectUri = "/" },
            CookieAuthenticationDefaults.AuthenticationScheme
        );
    }
    
    [HttpGet("signed-in-google")]
    public IActionResult SignedInGoogle()
    {
        var userEmail = HttpContext.User.FindFirstValue(ClaimTypes.Email);

        return Ok(new { email = userEmail });
    }
}