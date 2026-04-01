namespace Sinaf.Me.Data.Warhammer;

public partial class Unit
{
    public uint Id { get; set; }

    public uint GameModeId { get; set; }

    public string Name { get; set; } = null!;

    public byte Number { get; set; }

    public uint? Points { get; set; }

    public virtual GameMode GameMode { get; set; } = null!;

    public virtual ICollection<UnitCharacter> UnitCharacters { get; set; } = new List<UnitCharacter>();
}
