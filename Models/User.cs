using System;
using System.Collections.Generic;

namespace InForno.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = "user";

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
