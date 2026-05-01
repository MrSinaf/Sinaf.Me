using Microsoft.EntityFrameworkCore;
using Sinaf.Me.Data;

namespace Sinaf.Me.Components.Pages.Warhammer;

public partial class Gallery
{
	private uint[] characters = [];
	
	protected override async Task OnInitializedAsync()
	{
		await using var context = new WarhammerDbContext();
		characters = await context.Characters
								  .OrderByDescending(x => x.Id)
								  .Select(x => x.Id)
								  .ToArrayAsync();
	}
}