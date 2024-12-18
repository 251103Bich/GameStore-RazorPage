using System;
using System.Collections.Generic;

namespace Models;

public partial class MoneyHistory
{
    public string Mid { get; set; } = null!;

    public string Username { get; set; } = null!;

    public long Money { get; set; }

    public DateTime Date { get; set; }

    public string? Status { get; set; }

    public virtual Profile UsernameNavigation { get; set; } = null!;
}
