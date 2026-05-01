using System;
using System.Collections.Generic;

namespace Sinaf.Me.Data.Web;

public partial class ProjectLink
{
    public uint Id { get; set; }

    public uint ProjectId { get; set; }

    public string Name { get; set; } = null!;

    public string Url { get; set; } = null!;

    public byte Priority { get; set; }

    public bool IsIntern { get; set; }

    public virtual Project Project { get; set; } = null!;
}
