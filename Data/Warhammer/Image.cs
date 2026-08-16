using System;
using System.Collections.Generic;

namespace Sinaf.Me.Data.Warhammer;

public partial class Image
{
    public uint Id { get; set; }

    public string Path { get; set; } = null!;

    public uint GridX { get; set; }

    public uint GridY { get; set; }

    public DateTime CreatedAt { get; set; }
}
