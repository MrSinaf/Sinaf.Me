using System.Text.Json.Serialization;

namespace Sinaf.Me.Models.DawnOfWarDE;

public class LeaderboardStats
{
	[JsonPropertyName("statgroup_id")]
	public uint StatgroupId { get; init; }
	
	[JsonPropertyName("leaderboard_id")]
	public uint LeaderboardId { get; init; }
	
	[JsonPropertyName("wins")]
	public uint Wins { get; init; }
	
	[JsonPropertyName("losses")]
	public uint Losses { get; init; }
	
	[JsonPropertyName("streak")]
	public int Streak { get; init; }
	
	[JsonPropertyName("disputes")]
	public uint Disputes { get; init; }
	
	[JsonPropertyName("drops")]
	public uint Drops { get; init; }
	
	[JsonPropertyName("rank")]
	public int Rank { get; init; }
	
	[JsonPropertyName("ranktotal")]
	public int RankTotal { get; init; }
	
	[JsonPropertyName("ranklevel")]
	public int RankLevel { get; init; }
	
	[JsonPropertyName("rating")]
	public uint Rating { get; init; }
	
	[JsonPropertyName("lastmatchdate")]
	[JsonConverter(typeof(UnixTimestampDateTimeConverter))]
	public DateTime LastMatchDate { get; init; }
}