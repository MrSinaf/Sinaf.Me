using System;
using System.Collections.Generic;

namespace Sinaf.Me.Data.Warhammer;

public partial class CharacterPaint
{
    public uint Id { get; set; }

    public uint CharacterId { get; set; }

    public uint PaintId { get; set; }

    public string? Comment { get; set; }

    public virtual Character Character { get; set; } = null!;

    public virtual Paint Paint { get; set; } = null!;
}
