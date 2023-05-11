using System;
using System.Collections.Generic;

namespace momkitchen.Models;

public partial class FoodPackageInSession
{
    public int Id { get; set; }

    public int? FoodPackageId { get; set; }

    public int? SessionId { get; set; }

    public int? OrderId { get; set; }

    public int? Price { get; set; }

    public int? Quantity { get; set; }

    public int? RemainQuantity { get; set; }

    public bool? Status { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual FoodPackage? FoodPackage { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Session? Session { get; set; }
}
