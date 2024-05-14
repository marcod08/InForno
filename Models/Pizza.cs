using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InForno.Models;

public partial class Pizza
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual ICollection<Detail> Details { get; set; } = new List<Detail>();
}
