using System;
using System.Collections.Generic;

namespace Sinaf.Me.Data.Warhammer;

public partial class BattleUnitCharacter
{
    public uint Id { get; set; }

    public uint BattleUnitId { get; set; }

    public uint CharacterId { get; set; }

    public uint KillsParticipating { get; set; }

    public ulong IsDead { get; set; }

    public virtual BattleUnit BattleUnit { get; set; } = null!;

    public virtual Character Character { get; set; } = null!;
}
