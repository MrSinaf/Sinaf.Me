using System;
using System.Collections.Generic;

namespace Sinaf.Me.Data.Warhammer;

public partial class ClanPaint
{
    public uint Id { get; set; }

    public uint ClanId { get; set; }

    public uint PaintId { get; set; }

    public string? Comment { get; set; }

    public virtual Clan Clan { get; set; } = null!;

    public virtual Paint Paint { get; set; } = null!;
}
