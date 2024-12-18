using System;
using System.Collections.Generic;

namespace Models;

public partial class OrderDetail
{
    public string Odid { get; set; } = null!;

    public string Oid { get; set; } = null!;

    public string Gid { get; set; } = null!;

    public long Price { get; set; }

    public virtual Game GidNavigation { get; set; } = null!;

    public virtual ICollection<MoneyManagement> MoneyManagements { get; set; } = new List<MoneyManagement>();

    public virtual Order OidNavigation { get; set; } = null!;
}
