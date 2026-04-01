namespace Sinaf.Me.Data.Warhammer;

public partial class Character
{
    public uint Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Commentary { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<ClanCharacter> ClanCharacters { get; set; } = new List<ClanCharacter>();

    public virtual ICollection<UnitCharacter> UnitCharacters { get; set; } = new List<UnitCharacter>();
}
