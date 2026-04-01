using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Sinaf.Me.Data;
using Sinaf.Me.Data.Web;

namespace Sinaf.Me.Components.Pages.Projects;

public partial class Home : ComponentBase
{
	private Project[] projects = [];
	
	protected override async Task OnInitializedAsync()
	{
		await using var context = new WebDbContext();
		projects = await context.Projects.OrderByDescending(x => x.Order).ToArrayAsync();
	}
}