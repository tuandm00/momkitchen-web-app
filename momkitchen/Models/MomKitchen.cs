using System;
using System.Collections.Generic;

namespace momkitchen.Models;

public partial class MomKitchen
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? AccountStatus { get; set; }

    public int? BuildingId { get; set; }

    public virtual Building? Building { get; set; }

    public virtual ICollection<Dish> Dishes { get; } = new List<Dish>();
}
