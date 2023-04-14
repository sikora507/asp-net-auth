using Microsoft.AspNetCore.DataProtection;

namespace WebApi.Utilities;

public class CookieDecryptor
{
    private readonly IDataProtector _dataProtector;
    
    public CookieDecryptor(IDataProtectionProvider dataProtectionProvider)
    {
        _dataProtector =
            dataProtectionProvider.CreateProtector("Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationMiddleware",
                "Cookies", "v2");
    }
    
    public string DecryptCookie(HttpRequest request, string cookieName)
    {
        var encryptedCookie = request.Cookies[cookieName];
        if (string.IsNullOrEmpty(encryptedCookie))
        {
            return null;
        }
        try
        {
            var decryptedCookie = _dataProtector.Unprotect(encryptedCookie);
            
            return decryptedCookie;
        }
        catch (Exception)
        {
            return null;
        }
    }
}