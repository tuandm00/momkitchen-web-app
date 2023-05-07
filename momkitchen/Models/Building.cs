using System;
using System.Collections.Generic;

namespace momkitchen.Models;

public partial class Building
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Chef> Chefs { get; set; } = new List<Chef>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
