using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Sinaf.Me.Data;
using Sinaf.Me.Data.Warhammer;

namespace Sinaf.Me.Components.Pages.Warhammer;

public partial class Faction : ComponentBase
{
	[Inject] private NavigationManager Navigation { get; set; } = null!;
	[Parameter] public string ClanName { get; set; } = null!;
	
	private int selectedImage;
	
	private Clan? clan;
	private ListingCharacter[] characters = [];
	private Character? character;
	
	protected override async Task OnInitializedAsync()
	{
		await using var context = new WarhammerDbContext();
		clan = await context.Clans.Where(x => x.Name.ToLower() == ClanName.ToLower())
								.FirstOrDefaultAsync();
		if (clan == null)
			Navigation.NavigateTo($"/not-found?target={Navigation.Uri}");
		else
		{
			characters = await context.ListingCharacters.Where(x => x.ClanId == clan.Id)
									  .ToArrayAsync();
		}
	}
	
	private async Task Select(ListingCharacter? selected)
	{
		if (selected == null)
			character = null;
		else
		{
			await using var context = new WarhammerDbContext();
			character = await context.Characters
									 .Include(x => x.ClanCharacters)
									 .ThenInclude(x => x.Clan)
									 .Include(x => x.UnitCharacters)
									 .ThenInclude(x => x.Unit)
									 .ThenInclude(x => x.GameMode)
									 .FirstOrDefaultAsync(x => x.Id == selected.CharacterId);
		}
		selectedImage = 0;
		StateHasChanged();
	}
	
	private void ChangeSelection()
	{
		selectedImage = ++selectedImage % 3;
		StateHasChanged();
	}
}