using System;
using System.Collections.Generic;

namespace Sinaf.Me.Data.Warhammer;

public partial class CharacterDetail
{
    public uint Id { get; set; }

    public uint ClanId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int ThumbnailX { get; set; }

    public int ThumbnailY { get; set; }

    public byte ThumbnailS { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Unit { get; set; } = null!;

    public string UnitType { get; set; } = null!;

    public byte OrderType { get; set; }

    public byte OrderUnit { get; set; }
}
