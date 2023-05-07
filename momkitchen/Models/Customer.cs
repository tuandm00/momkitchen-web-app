using System;
using System.Collections.Generic;

namespace momkitchen.Models;

public partial class Customer
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public string? Image { get; set; }

    public string? Email { get; set; }

    public string? DefaultBuilding { get; set; }

    public virtual Account? EmailNavigation { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
