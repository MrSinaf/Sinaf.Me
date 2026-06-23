using System.Text.Json.Serialization;

namespace Sinaf.Me.Models.DawnOfWarDE;

public class MatchHistory
{
	[JsonPropertyName("id")]
	public uint Id { get; init; }

	[JsonPropertyName("creator_profile_id")]
	public uint CreatorProfileId { get; init; }

	[JsonPropertyName("mapname")]
	public string MapName { get; init; } = string.Empty;

	[JsonPropertyName("maxplayers")]
	public uint MaxPlayers { get; init; }

	[JsonPropertyName("matchtype_id")]
	public uint MatchTypeId { get; init; }

	[JsonPropertyName("options")]
	public string Options { get; init; } = string.Empty;

	[JsonPropertyName("slotinfo")]
	public string SlotInfo { get; init; } = string.Empty;

	[JsonPropertyName("description")]
	public string Description { get; init; } = string.Empty;

	[JsonPropertyName("startgametime")]	
	[JsonConverter(typeof(UnixTimestampDateTimeConverter))]
	public DateTime StartGameTime { get; init; }

	[JsonPropertyName("completiontime")]
	[JsonConverter(typeof(UnixTimestampDateTimeConverter))]
	public DateTime CompletionTime { get; init; }

	[JsonPropertyName("observertotal")]
	public uint ObserverTotal { get; init; }

	[JsonPropertyName("matchhistoryreportresults")]
	public IReadOnlyList<MatchHistoryReportResult> MatchHistoryReportResults { get; init; } = [];

	[JsonPropertyName("matchhistorymember")]
	public IReadOnlyList<MatchHistoryMember> MatchHistoryMembers { get; init; } = [];
}

