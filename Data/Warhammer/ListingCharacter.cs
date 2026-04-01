namespace Sinaf.Me.Data.Warhammer;

public partial class ListingCharacter
{
    public string Clan { get; set; } = null!;

    public string? Name { get; set; }

    public string UnitName { get; set; } = null!;

    public string GameMode { get; set; } = null!;

    public uint CharacterId { get; set; }

    public uint ClanId { get; set; }

    public uint UnitId { get; set; }

    public uint GameModeId { get; set; }
}
