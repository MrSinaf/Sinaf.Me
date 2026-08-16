using System;
using System.Collections.Generic;

namespace Sinaf.Me.Data.Warhammer;

public partial class Character
{
    public uint Id { get; set; }

    public uint ClanId { get; set; }

    public uint UnitId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int ThumbnailX { get; set; }

    public int ThumbnailY { get; set; }

    public byte ThumbnailS { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<BattleUnitsCharacter> BattleUnitsCharacters { get; set; } = new List<BattleUnitsCharacter>();

    public virtual Clan Clan { get; set; } = null!;

    public virtual Unit Unit { get; set; } = null!;
}
