namespace Sinaf.Me.Data.Warhammer;

public partial class Faction
{
    public uint Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Clan> Clans { get; set; } = new List<Clan>();

    public virtual ICollection<GameMode> GameModes { get; set; } = new List<GameMode>();
}
