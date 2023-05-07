using System;
using System.Collections.Generic;

namespace momkitchen.Models;

public partial class FoodPackage
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Image { get; set; }

    public decimal? DefaultPrice { get; set; }

    public int? ChefId { get; set; }

    public string? Description { get; set; }

    public virtual Chef? Chef { get; set; }

    public virtual ICollection<DishFoodPackage> DishFoodPackages { get; set; } = new List<DishFoodPackage>();

    public virtual ICollection<FoodPackageInSession> FoodPackageInSessions { get; set; } = new List<FoodPackageInSession>();

    public virtual ICollection<FoodPackageStyle> FoodPackageStyles { get; set; } = new List<FoodPackageStyle>();
}
