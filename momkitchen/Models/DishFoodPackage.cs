using System;
using System.Collections.Generic;

namespace momkitchen.Models;

public partial class DishFoodPackage
{
    public int DishId { get; set; }

    public int FoodPackageId { get; set; }

    public int? Quantity { get; set; }

    public int? DisplayIndex { get; set; }

    public virtual Dish Dish { get; set; } = null!;

    public virtual FoodPackage FoodPackage { get; set; } = null!;
}
