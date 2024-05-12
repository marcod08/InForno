using System;
using System.Collections.Generic;

namespace InForno.Models;

public partial class Order
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Address { get; set; } = null!;

    public string? Note { get; set; }

    public bool Fullfilled { get; set; }

    public virtual ICollection<Detail> Details { get; set; } = new List<Detail>();

    public virtual User User { get; set; } = null!;
}
