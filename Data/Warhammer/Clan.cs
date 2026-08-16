using System;
using System.Collections.Generic;

namespace Sinaf.Me.Data.Warhammer;

public partial class Clan
{
    public uint Id { get; set; }

    public uint RaceId { get; set; }

    public string UniqueName { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Currency { get; set; } = null!;

    public virtual ICollection<Character> Characters { get; set; } = new List<Character>();

    public virtual Race Race { get; set; } = null!;
}
