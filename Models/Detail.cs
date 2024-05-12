using System;
using System.Collections.Generic;

namespace InForno.Models;

public partial class Detail
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int PizzaId { get; set; }

    public int? DrinkId { get; set; }

    public int PizzaQuantity { get; set; }

    public int? DrinkQuantity { get; set; }

    public decimal TotalPrice { get; set; }

    public virtual Drink? Drink { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Pizza Pizza { get; set; } = null!;
}
