using System;
using System.Collections.Generic;

namespace Sinaf.Me.Data.Warhammer;

public partial class Character
{
    public uint Id { get; set; }

    public uint? ClanId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Commentary { get; set; }

    public int ThumbnailX { get; set; }

    public int ThumbnailY { get; set; }

    public byte ThumbnailS { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool Published { get; set; }

    public virtual ICollection<BattleUnitCharacter> BattleUnitCharacters { get; set; } = new List<BattleUnitCharacter>();

    public virtual ICollection<CharacterPaint> CharacterPaints { get; set; } = new List<CharacterPaint>();

    public virtual Clan? Clan { get; set; }

    public virtual ICollection<Unit> Units { get; set; } = new List<Unit>();
}
