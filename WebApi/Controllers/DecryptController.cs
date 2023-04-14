using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Utilities;

namespace WebApi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class DecryptController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        var cookieDecryptor = HttpContext.RequestServices.GetService<CookieDecryptor>();
        string decryptedCookie = cookieDecryptor.DecryptCookie(HttpContext.Request, ".AspNetCore.Cookies");
        
        return Ok(new {decrypted = decryptedCookie});
    }
}