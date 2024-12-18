using System;
using System.Collections.Generic;

namespace Models;

public partial class Feedback
{
    public string Username { get; set; } = null!;

    public string Gid { get; set; } = null!;

    public string Feedback1 { get; set; } = null!;

    public int Rate { get; set; }

    public string Status { get; set; } = null!;

    public DateTime Date { get; set; }

    public virtual Game GidNavigation { get; set; } = null!;

    public virtual Profile UsernameNavigation { get; set; } = null!;
}
