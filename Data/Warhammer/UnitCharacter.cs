namespace Sinaf.Me.Data.Warhammer;

public partial class UnitCharacter
{
    public uint Id { get; set; }

    public uint UnitId { get; set; }

    public uint CharacterId { get; set; }

    public virtual Character Character { get; set; } = null!;

    public virtual Unit Unit { get; set; } = null!;
}
