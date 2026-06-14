using System;
using System.Collections.Generic;

namespace Sinaf.Me.Data.Warhammer;

public partial class Player
{
    public uint Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<BattlePlayer> BattlePlayers { get; set; } = new List<BattlePlayer>();
}
