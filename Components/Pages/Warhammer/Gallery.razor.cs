using Microsoft.EntityFrameworkCore;
using Sinaf.Me.Data;

namespace Sinaf.Me.Components.Pages.Warhammer;

public partial class Gallery
{
	private const int PAGE_SIZE = 10;
	
	private uint[] characters = [];
	private int skip;
	private bool hasMore = true;
	private bool loading;
	
	protected override async Task OnInitializedAsync() => await LoadMoreAsync();
	
	private async Task LoadMoreAsync()
	{
		if (loading || !hasMore)
			return;
		
		loading = true;
		
		await using var context = new WarhammerDbContext();
		var data = await context.Characters
								.OrderByDescending(x => x.Id)
								.Skip(skip)
								.Take(PAGE_SIZE)
								.Select(x => x.Id)
								.ToArrayAsync();
		
		characters = [..characters, ..data];
		skip += data.Length;
		hasMore = data.Length == PAGE_SIZE;
		
		loading = false;
	}
}