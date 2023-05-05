using Microsoft.AspNetCore.Authentication;
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
            new AuthenticationProperties { RedirectUri = "/" },
            GoogleDefaults.AuthenticationScheme
        );
    }
}