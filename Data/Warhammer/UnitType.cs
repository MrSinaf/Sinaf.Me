using System;
using System.Collections.Generic;

namespace Sinaf.Me.Data.Warhammer;

public partial class UnitType
{
    public uint Id { get; set; }

    public string Name { get; set; } = null!;

    public byte Order { get; set; }

    public virtual ICollection<Unit> Units { get; set; } = new List<Unit>();
}
