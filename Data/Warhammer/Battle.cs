using System;
using System.Collections.Generic;

namespace Sinaf.Me.Data.Warhammer;

public partial class Battle
{
    public uint Id { get; set; }

    public uint GameId { get; set; }

    public DateTime Date { get; set; }

    public uint? Points { get; set; }

    public virtual ICollection<BattlePlayer> BattlePlayers { get; set; } = new List<BattlePlayer>();

    public virtual Game Game { get; set; } = null!;
}
