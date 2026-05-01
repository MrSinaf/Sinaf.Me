using System;
using System.Collections.Generic;

namespace Sinaf.Me.Data.Warhammer;

public partial class Character
{
    public uint Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Commentary { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
