using System.Text.Json.Serialization;

namespace Sinaf.Me.Models.DawnOfWarDE;

public class MatchHistoryMember
{
	[JsonPropertyName("id")]
	public uint MatchHistoryId { get; init; }
	
	[JsonPropertyName("profile_id")]
	public uint ProfileId { get; init; }
	
	[JsonPropertyName("race_id")]
	public uint RaceId { get; init; }
	
	[JsonPropertyName("statgroup_id")]
	public uint StatGroupId { get; init; }
	
	[JsonPropertyName("teamid")]
	public uint TeamId { get; init; }
	
	[JsonPropertyName("wins")]
	public uint Wins { get; init; }
	
	[JsonPropertyName("losses")]
	public uint Losses { get; init; }
	
	[JsonPropertyName("streak")]
	public int Streak { get; init; }
	
	[JsonPropertyName("arbitration")]
	public uint Arbitration { get; init; }
	
	[JsonPropertyName("outcome")]
	public uint Outcome { get; init; }
	
	[JsonPropertyName("oldrating")]
	public uint OldRating { get; init; }
	
	[JsonPropertyName("newrating")]
	public uint NewRating { get; init; }
	
	[JsonPropertyName("reporttype")]
	public uint ReportType { get; init; }
}