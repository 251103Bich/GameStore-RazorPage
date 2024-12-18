using System;
using System.Collections.Generic;

namespace Models;

public partial class Category
{
    public string Cid { get; set; } = null!;

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Game> Gids { get; set; } = new List<Game>();
}
