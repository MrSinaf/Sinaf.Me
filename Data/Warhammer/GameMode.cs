namespace Sinaf.Me.Data.Warhammer;

public partial class GameMode
{
    public uint Id { get; set; }

    public uint FactionId { get; set; }

    public string Name { get; set; } = null!;

    public virtual Faction Faction { get; set; } = null!;

    public virtual ICollection<Unit> Units { get; set; } = new List<Unit>();
}
