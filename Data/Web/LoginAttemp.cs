using System;
using System.Collections.Generic;

namespace Sinaf.Me.Data.Web;

public partial class LoginAttemp
{
    public uint Id { get; set; }

    public string Ip { get; set; } = null!;

    public DateTime Date { get; set; }

    public bool Success { get; set; }
}
