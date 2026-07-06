using System;
using System.Collections.Generic;

namespace Sinaf.Me.Data.Warhammer;

public partial class PaintSubType
{
    public uint Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Paint> Paints { get; set; } = new List<Paint>();
}
