using System;
using System.Collections.Generic;

namespace Sinaf.Me.Data.Web;

public partial class Blog
{
    public uint Id { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime? PublishAt { get; set; }

    public bool Published { get; set; }
}
