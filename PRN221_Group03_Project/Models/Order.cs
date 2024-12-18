using System;
using System.Collections.Generic;

namespace Models;

public partial class Order
{
    public string Oid { get; set; } = null!;

    public string Username { get; set; } = null!;

    public long Total { get; set; }

    public DateTime Date { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Profile UsernameNavigation { get; set; } = null!;
}
