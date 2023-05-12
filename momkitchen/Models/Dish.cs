using System;
using System.Collections.Generic;

namespace momkitchen.Models;

public partial class Dish
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? DishTypeId { get; set; }

    public int? ChefId { get; set; }

    public string? Image { get; set; }

    public virtual Chef? Chef { get; set; }

    public virtual ICollection<DishFoodPackage> DishFoodPackages { get; set; } = new List<DishFoodPackage>();

    public virtual DishType? DishType { get; set; }
}
