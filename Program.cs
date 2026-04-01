using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Sinaf.Me.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
	   .AddInteractiveServerComponents();

builder.Services.AddHttpClient();
builder.Services.AddControllers();

builder.Services
	   .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	   .AddCookie(options =>
	   {
		   options.LoginPath = "/login";
		   options.AccessDeniedPath = "/login";
		   options.Cookie.Name = "Sinaf.Auth";
		   options.Cookie.HttpOnly = true;
		   options.Cookie.SameSite = SameSiteMode.Lax;
		   options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // Mettre ça en Always
	   });

builder.Services.AddAuthorization();
builder.Services.AddAntiforgery();
builder.Services.AddCascadingAuthenticationState();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/error", createScopeForErrors: true);
	app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.MapControllers();
app.MapPost(
	"/api/auth/logout",
	async (HttpContext context) =>
	{
		await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
		return Results.Ok();
	}
);

app.Run();