using System;
using System.Collections.Generic;

namespace Sinaf.Me.Data.Warhammer;

public partial class Paint
{
    public uint Id { get; set; }

    public string Name { get; set; } = null!;

    public string Hex { get; set; } = null!;

    public uint TypeId { get; set; }

    public uint? SubTypeId { get; set; }

    public virtual ICollection<CharacterPaint> CharacterPaints { get; set; } = new List<CharacterPaint>();

    public virtual ICollection<ClanPaint> ClanPaints { get; set; } = new List<ClanPaint>();

    public virtual PaintSubType? SubType { get; set; }

    public virtual PaintType Type { get; set; } = null!;
}
