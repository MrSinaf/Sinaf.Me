using System;
using System.Collections.Generic;

namespace Sinaf.Me.Data.Warhammer;

public partial class BattleUnit
{
    public uint Id { get; set; }

    public uint BattlePlayerId { get; set; }

    public uint Kills { get; set; }

    public uint Scores { get; set; }

    public uint FailedCharges { get; set; }

    public uint ImpossibleSaves { get; set; }

    public uint DamageDone { get; set; }

    public uint DamageTaken { get; set; }

    public uint DamageBlocked { get; set; }

    public virtual BattlePlayer BattlePlayer { get; set; } = null!;

    public virtual ICollection<BattleUnitsCharacter> BattleUnitsCharacters { get; set; } = new List<BattleUnitsCharacter>();
}
