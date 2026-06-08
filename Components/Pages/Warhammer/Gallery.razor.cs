using Microsoft.EntityFrameworkCore;
using Sinaf.Me.Data;
using Sinaf.Me.Data.Warhammer;

namespace Sinaf.Me.Components.Pages.Warhammer;

public partial class Gallery
{
	private Character[] characters = [];
	
	protected override async Task OnInitializedAsync()
	{
		await using var context = new WarhammerDbContext();
		characters = await context.Characters
								  .OrderByDescending(x => x.Id)
								  .Select(x => new Character
								  {
									  Id = x.Id,
									  Name = x.Name,
									  UpdatedAt = x.UpdatedAt
								  })
								  .ToArrayAsync();
	}
}