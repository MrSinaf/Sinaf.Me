using System;
using System.Collections.Generic;

namespace Sinaf.Me.Data.Web;

public partial class Project
{
    public uint Id { get; set; }

    public string UniqueName { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool HasPage { get; set; }

    public string? Github { get; set; }

    public uint Order { get; set; }

    public virtual ICollection<ProjectRepository> ProjectRepositories { get; set; } = new List<ProjectRepository>();
}
