using Microsoft.EntityFrameworkCore;
using Sinaf.Me.Data;
using Sinaf.Me.Data.Web;

namespace Sinaf.Me.Components.Pages.Blogs;

public partial class Home
{
	private Blog[]? blogs;
	
	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (!firstRender)
			return;
		
		await using var context = new WebDbContext();
		blogs = await context.Blogs
							 .Select(x => new Blog
							 {
								 Id = x.Id,
								 Title = x.Title,
								 Content = x.Content.Length < 128
										 ? x.Content
										 : x.Content.Substring(0, 128),
								 PublishAt = x.PublishAt,
								 Published = x.Published
							 })
							 .Where(x => x.Published && x.PublishAt <= DateTime.Now)
							 .OrderByDescending(x => x.PublishAt)
							 .ToArrayAsync();
		StateHasChanged();
	}
}