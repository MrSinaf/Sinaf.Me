using System;
using System.Collections.Generic;

namespace Sinaf.Me.Data.Warhammer;

public partial class BattlePlayer
{
    public uint Id { get; set; }

    public uint BattleId { get; set; }

    public uint PlayerId { get; set; }

    public virtual Battle Battle { get; set; } = null!;

    public virtual ICollection<BattleUnit> BattleUnits { get; set; } = new List<BattleUnit>();

    public virtual Player Player { get; set; } = null!;
}
