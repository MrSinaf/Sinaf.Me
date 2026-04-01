using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sinaf.Me.Data;

namespace Sinaf.Me.Controllers;

[ApiController]
[Route("/webhooks")]
public class WebhooksController : Controller
{
	[HttpPost("github/push")]
	public async Task<IActionResult> NewGithubPush()
	{
		using var reader = new StreamReader(Request.Body);
		var root = JsonDocument.Parse(await reader.ReadToEndAsync()).RootElement;
		
		if (root.GetProperty("sender").GetProperty("login").GetString() != "MrSinaf")
			return Ok("Not MrSinaf");
		
		await using var context = new WebDbContext();
		var repository = await context.ProjectRepositories.FirstOrDefaultAsync(x => x.Repository ==
																root.GetProperty("repository")
																		.GetProperty("name")
																		.GetString()
									  );
		if (repository == null)
			return Ok("Repository not found in sinaf.me");
		
		var headerCommit = root.GetProperty("head_commit");
		var commit = headerCommit.GetProperty("message").GetString()!;
		
		repository.Update = DateTime.Now;
		repository.Branch = root.GetProperty("ref").GetString()!.Replace("refs/heads/", "");
		repository.Commit = (commit.Length > 128 ? commit[..128] : commit).Split('\n')[0];
		repository.Added = (uint)headerCommit.GetProperty("added").GetArrayLength();
		repository.Removed = (uint)headerCommit.GetProperty("removed").GetArrayLength();
		repository.Modified = (uint)headerCommit.GetProperty("modified").GetArrayLength();
		await context.SaveChangesAsync();
		
		return Ok();
	}
}