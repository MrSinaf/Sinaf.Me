using Microsoft.EntityFrameworkCore;
using Sinaf.Me.Data;
using Sinaf.Me.Data.Web;
using Steam.Models.SteamCommunity;
using SteamWebAPI2.Interfaces;
using SteamWebAPI2.Utilities;

namespace Sinaf.Me.Components.Pages;

public partial class Home
{
	private (string urlIcon, string status, string statusType)? steamProfil;
	private (string name, string icon, string hours)? currentSteamGameInfos;
	private ProjectRepository? lastPush;
	private Project[] projects = [];
	private Presence? presence;
	
	protected override async Task OnInitializedAsync()
	{
		var webInterfaceFactory = new SteamWebInterfaceFactory("8E1A35F9D5533AAE3CD66A8E68ABF120");
		
		var steamInterface = webInterfaceFactory
				.CreateSteamWebInterface<SteamUser>(new HttpClient());
		var steamSummary = (await steamInterface.GetPlayerSummaryAsync(76561199117557684)).Data;
		
		if (!string.IsNullOrEmpty(steamSummary.PlayingGameName))
		{
			var steamPlayerInterface = webInterfaceFactory.CreateSteamWebInterface<PlayerService>();
			var games = await steamPlayerInterface.GetOwnedGamesAsync(
				76561199117557684,
				includeAppInfo: true,
				includeFreeGames: true,
				appIdsToFilter: [uint.Parse(steamSummary.PlayingGameId)]
			);
			
			var game = games.Data.OwnedGames.First();
			var hours = game.PlaytimeForever.TotalHours;
			var minutes = game.PlaytimeForever.Minutes;
			currentSteamGameInfos = (
				game.Name,
				"https://steamcdn-a.akamaihd.net/steamcommunity/public/images/apps/" +
				$"{game.AppId}/{game.ImgIconUrl}.jpg",
				$"Temps de jeu : {hours:0} heure{(hours > 1 ? "s" : "")} et " +
				$"{minutes:0} minute{(minutes > 1 ? "s" : "")} "
			);
		}
		steamProfil = (steamSummary.AvatarMediumUrl, steamSummary.UserStatus switch
		{
			UserStatus.Offline => "Déconnecté",
			UserStatus.Online  => "En ligne",
			UserStatus.Busy    => "Occupé",
			UserStatus.Away    => "Absent",
			UserStatus.Snooze  => "Zzz",
			UserStatus.Unknown => "Inconnu",
			UserStatus.InGame  => "En jeu",
			_                  => throw new ArgumentOutOfRangeException()
		}, steamSummary.UserStatus.ToString().ToLower());
		
		await using var context = new WebDbContext();
		lastPush = await context.ProjectRepositories
								.OrderByDescending(x => x.Update)
								.FirstAsync();
		projects = await context.Projects.Include(x => x.ProjectLinks)
								.OrderByDescending(x => x.Order).Take(3).ToArrayAsync();
		var presences = await context.Presences.ToArrayAsync();
		presence = presences[Random.Shared.Next(presences.Length)];
	}
}