namespace Sinaf.Me.Data.Warhammer;

public partial class Clan
{
    public uint Id { get; set; }

    public uint FactionId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ClanCharacter> ClanCharacters { get; set; } = new List<ClanCharacter>();

    public virtual Faction Faction { get; set; } = null!;
}
