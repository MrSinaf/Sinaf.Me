using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sinaf.Me.Data;
using Sinaf.Me.Data.Web;

namespace Sinaf.Me.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Auth : Controller
{
	[Route("login")]
	public async Task<IActionResult> Index()
	{
		var form = await Request.ReadFormAsync();
		var password = form["password"].ToString();
		
		await using var context = new WebDbContext();
		var loginAttemp = new LoginAttemp
		{
			Ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty,
			Date = DateTime.Now
		};
		
		var isNotAutorized = (await context.LoginAttemps.OrderByDescending(x => x.Date)
										   .Where(x => x.Ip == loginAttemp.Ip)
										   .Take(3)
										   .ToArrayAsync()).Count(x => !x.Success) == 3;
		
		await context.LoginAttemps.AddAsync(loginAttemp);
		if (string.IsNullOrEmpty(loginAttemp.Ip)
			|| isNotAutorized
			|| password != Environment.GetEnvironmentVariable("PASS"))
		{
			await context.SaveChangesAsync();
			return Redirect("/login");
		}
		
		loginAttemp.Success = true;
		await context.SaveChangesAsync();
		
		var identity = new ClaimsIdentity(
			new List<Claim>
			{
				new (ClaimTypes.Name, "admin")
			},
			CookieAuthenticationDefaults.AuthenticationScheme
		);
		
		var principal = new ClaimsPrincipal(identity);
		
		await HttpContext.SignInAsync(
			CookieAuthenticationDefaults.AuthenticationScheme,
			principal
		);
		
		return Redirect("/admin");
	}
	
	[Route("logout")]
	public async Task<IActionResult> Logout()
	{
		await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
		return Redirect("/login");
	}
}