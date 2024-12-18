using System;
using System.Collections.Generic;

namespace Models;

public partial class Game
{
    public string Gid { get; set; } = null!;

    public string GameName { get; set; } = null!;

    public long Price { get; set; }

    public string Picture { get; set; } = null!;

    public DateTime Date { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string Description { get; set; } = null!;

    public string Configuration { get; set; } = null!;

    public string SellerName { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string DownloadFile { get; set; } = null!;

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Category> Cids { get; set; } = new List<Category>();

    public virtual ICollection<Profile> Usernames { get; set; } = new List<Profile>();

    public virtual ICollection<Profile> UsernamesNavigation { get; set; } = new List<Profile>();
}
