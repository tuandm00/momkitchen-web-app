using System;
using System.Collections.Generic;

namespace momkitchen.Models;

public partial class OrderDetail
{
    public int SessionPackageId { get; set; }

    public double Price { get; set; }

    public int Quantity { get; set; }

    public string Status { get; set; } = null!;

    public int OrderId { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual SessionPackage SessionPackage { get; set; } = null!;
}
