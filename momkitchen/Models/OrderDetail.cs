using System;
using System.Collections.Generic;

namespace momkitchen.Models;

public partial class OrderDetail
{
    public int Id { get; set; }

    public int? FoodPackageInSessionId { get; set; }

    public double? Price { get; set; }

    public int? Quantity { get; set; }

    public string? Status { get; set; }

    public int? OrderId { get; set; }

    public virtual FoodPackageInSession? FoodPackageInSession { get; set; }

    public virtual Order? Order { get; set; }
}
