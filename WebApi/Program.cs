using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.OAuth;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.local.json", true);

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
        options.ClientId = builder.Configuration.GetValue<string>("Authentication:Google:ClientId") ?? "";
        options.ClientSecret = builder.Configuration.GetValue<string>("Authentication:Google:ClientSecret") ?? "";
        options.CallbackPath = "/signin-google"; 
        options.AuthorizationEndpoint += "?prompt=select_account";
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