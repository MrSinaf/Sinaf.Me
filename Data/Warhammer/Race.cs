using System;
using System.Collections.Generic;

namespace Sinaf.Me.Data.Warhammer;

public partial class Race
{
    public uint Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Army> Armies { get; set; } = new List<Army>();
}
