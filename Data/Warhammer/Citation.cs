using System;
using System.Collections.Generic;

namespace Sinaf.Me.Data.Warhammer;

public partial class Citation
{
    public uint Id { get; set; }

    public string Content { get; set; } = null!;

    public string Source { get; set; } = null!;
}
