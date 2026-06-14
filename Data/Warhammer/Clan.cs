using System;
using System.Collections.Generic;

namespace Sinaf.Me.Data.Warhammer;

public partial class Clan
{
    public uint Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Character> Characters { get; set; } = new List<Character>();
}
