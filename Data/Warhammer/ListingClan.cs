namespace Sinaf.Me.Data.Warhammer;

public partial class ListingClan
{
    public string Name { get; set; } = null!;

    public string Faction { get; set; } = null!;

    public long CharactersN { get; set; }

    public uint ClanId { get; set; }
}
