using System.Text.Json.Serialization;

namespace Sinaf.Me.Models.DawnOfWarDE;

public class MatchHistoryReportResult
{
	[JsonPropertyName("matchhistory_id")]
	public uint MatchHistoryId { get; init; }
	
	[JsonPropertyName("profile_id")]
	public uint ProfileId { get; init; }
	
	[JsonPropertyName("resulttype")]
	public uint ResultType { get; init; }
	
	[JsonPropertyName("teamid")]
	public uint TeamId { get; init; }
	
	[JsonPropertyName("race_id")]
	public uint RaceId { get; init; }
	
	[JsonPropertyName("xpgained")]
	public uint XpGained { get; init; }
	
	[JsonPropertyName("counters")]
	public string Counters { get; init; } = string.Empty;
	
	[JsonPropertyName("matchstartdate")]
	[JsonConverter(typeof(UnixTimestampDateTimeConverter))]
	public DateTime MatchStartDate { get; init; }
}