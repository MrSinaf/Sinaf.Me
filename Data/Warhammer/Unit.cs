using System;
using System.Collections.Generic;

namespace Sinaf.Me.Data.Warhammer;

public partial class Unit
{
    public uint Id { get; set; }

    public uint ArmieId { get; set; }

    public string Name { get; set; } = null!;

    public uint Number { get; set; }

    public uint? Cost { get; set; }

    public virtual Army Armie { get; set; } = null!;

    public virtual ICollection<Character> Characters { get; set; } = new List<Character>();
}
