using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.OAuth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/signin";
    })
    .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
    {
        options.ClientId = "269492224424-9oag6eaooneq3fu8j2hu7qops3d8i5c0.apps.googleusercontent.com";
        options.ClientSecret = "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";
        options.CallbackPath = "/signin-google"; 
        options.Events = new OAuthEvents
        {
            OnTicketReceived = async context =>
            {
                // You can access the user's email here
                var email = context.Principal.FindFirstValue(ClaimTypes.Email);
            
                // Store the email in the user's claims
                var claims = new List<Claim>
                {
                    new Claim("Email", email)
                };
                var appIdentity = new ClaimsIdentity(claims);
                context.Principal.AddIdentity(appIdentity);

                await Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();