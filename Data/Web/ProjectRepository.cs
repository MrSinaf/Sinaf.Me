using System;
using System.Collections.Generic;

namespace Sinaf.Me.Data.Web;

public partial class ProjectRepository
{
    public uint Id { get; set; }

    public uint ProjectId { get; set; }

    public string Repository { get; set; } = null!;

    public string Branch { get; set; } = null!;

    public string Commit { get; set; } = null!;

    public uint Added { get; set; }

    public uint Removed { get; set; }

    public uint Modified { get; set; }

    public DateTime Update { get; set; }

    public virtual Project Project { get; set; } = null!;
}
