using System.Text.Json.Serialization;

namespace Sinaf.Me.Models.DawnOfWarDE;

public class Profile
{
	[JsonPropertyName("profile_id")]
	public uint ProfileId { get; init; }
	
	[JsonPropertyName("name")]
	public string Name { get; init; }
	
	[JsonPropertyName("alias")]
	public string Alias { get; init; }
	
	[JsonPropertyName("personal_statgroup_id")]
	public uint PersonalStatGroupId { get; init; }
	
	[JsonPropertyName("xp")]
	public uint Xp { get; init; }
	
	[JsonPropertyName("level")]
	public uint Level { get; init; }
	
	[JsonPropertyName("leaderboardregion_id")]
	public int LeaderboardRegionId { get; init; }
	
	[JsonPropertyName("country")]
	public string Country { get; init; }
}