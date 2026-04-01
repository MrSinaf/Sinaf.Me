using Microsoft.EntityFrameworkCore;
using Sinaf.Me.Data;
using Sinaf.Me.Data.Web;

namespace Sinaf.Me.Components.Pages;

public partial class Home
{
	private ProjectRepository? lastPush;
	private Project[] projects = [];
	
	protected override async Task OnInitializedAsync()
	{
		await using var context = new WebDbContext();
		lastPush = await context.ProjectRepositories
										 .OrderByDescending(x => x.Update)
										 .FirstAsync();
		projects = await context.Projects.Take(3).OrderByDescending(x => x.Order).ToArrayAsync();
	}
}