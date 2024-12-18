using System;
using System.Collections.Generic;

namespace Models;

public partial class Profile
{
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Fullname { get; set; }

    public string? Gender { get; set; }

    public DateTime? Birthday { get; set; }

    public long Money { get; set; }

    public string Type { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime Date { get; set; }

    public string? Token { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<MoneyHistory> MoneyHistories { get; set; } = new List<MoneyHistory>();

    public virtual ICollection<MoneyManagement> MoneyManagements { get; set; } = new List<MoneyManagement>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Game> Gids { get; set; } = new List<Game>();

    public virtual ICollection<Game> GidsNavigation { get; set; } = new List<Game>();
}
