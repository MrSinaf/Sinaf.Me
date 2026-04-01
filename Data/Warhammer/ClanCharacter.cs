namespace Sinaf.Me.Data.Warhammer;

public partial class ClanCharacter
{
    public uint Id { get; set; }

    public uint ClanId { get; set; }

    public uint CharacterId { get; set; }

    public virtual Character Character { get; set; } = null!;

    public virtual Clan Clan { get; set; } = null!;
}
