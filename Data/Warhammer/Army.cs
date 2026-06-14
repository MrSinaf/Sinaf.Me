using System;
using System.Collections.Generic;

namespace Sinaf.Me.Data.Warhammer;

public partial class Army
{
    public uint Id { get; set; }

    public uint GameId { get; set; }

    public uint RaceId { get; set; }

    public string Name { get; set; } = null!;

    public virtual Game Game { get; set; } = null!;

    public virtual Race Race { get; set; } = null!;

    public virtual ICollection<Unit> Units { get; set; } = new List<Unit>();
}
