using System;
using System.Collections.Generic;

namespace momkitchen.Models;

public partial class Chef
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public string? Image { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public int? BuildingId { get; set; }

    public virtual Building? Building { get; set; }

    public virtual ICollection<Dish> Dishes { get; set; } = new List<Dish>();

    public virtual Account? EmailNavigation { get; set; }

    public virtual ICollection<FoodPackage> FoodPackages { get; set; } = new List<FoodPackage>();
}
