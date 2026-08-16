using System;
using System.Collections.Generic;

namespace Sinaf.Me.Data.Warhammer;

public partial class Unit
{
    public uint Id { get; set; }

    public uint TypeId { get; set; }

    public uint RaceId { get; set; }

    public string Name { get; set; } = null!;

    public uint Number { get; set; }

    public byte Order { get; set; }

    public virtual ICollection<Character> Characters { get; set; } = new List<Character>();

    public virtual Race Race { get; set; } = null!;

    public virtual UnitType Type { get; set; } = null!;
}
