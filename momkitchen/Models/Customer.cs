using System;
using System.Collections.Generic;

namespace momkitchen.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string Phone { get; set; } = null!;

    public string? Image { get; set; }

    public string Email { get; set; } = null!;

    public int? DefaultBuilding { get; set; }

    public virtual Account EmailNavigation { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
