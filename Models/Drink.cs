﻿using System;
using System.Collections.Generic;

namespace InForno.Models;

public partial class Drink
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual ICollection<Detail> Details { get; set; } = new List<Detail>();
}
