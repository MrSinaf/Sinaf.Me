using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using Sinaf.Me.Components.Customs;
using Sinaf.Me.Data;
using Sinaf.Me.Data.Web;

namespace Sinaf.Me.Components.Pages.Blogs;

public partial class Home
{
	[Inject] private IJSRuntime JS { get; set; } = null!;
	[Inject] private AuthenticationStateProvider ASP { get; set; } = null!;
	
	private Blog[]? blogs;
	private bool isPervert;
	
	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (!firstRender)
			return;
		
		var isConnected = (await ASP.GetAuthenticationStateAsync()).User.Identity?.IsAuthenticated
						  ?? false;
		await using var context = new WebDbContext();
		blogs = await context.Blogs
							 .Select(x => new Blog
							 {
								 Id = x.Id,
								 Title = x.Title,
								 Content = (x.Content.Length < 128
										 ? x.Content
										 : x.Content.Substring(0, 128) + "...").Replace("\n", ""),
								 PublishAt = x.PublishAt,
								 Published = x.Published
							 })
							 .Where(x => isConnected || x.Published)
							 .OrderByDescending(x => x.PublishAt)
							 .ToArrayAsync();
		foreach (var blog in blogs)
			blog.Content = blog.Content.Split('#')[0];
		
		isPervert = await PervertGate.GetConfirmed(JS);
		StateHasChanged();
	}
}