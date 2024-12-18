using System;
using System.Collections.Generic;

namespace Models;

public partial class MoneyManagement
{
    public string Odid { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Admin { get; set; } = null!;

    public long SellerMoney { get; set; }

    public long Admoney { get; set; }

    public DateTime Date { get; set; }

    public virtual OrderDetail Od { get; set; } = null!;

    public virtual Profile UsernameNavigation { get; set; } = null!;
}
