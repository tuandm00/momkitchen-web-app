using System;
using System.Collections.Generic;

namespace momkitchen.Models;

public partial class OrderTracking
{
    public int Id { get; set; }

    public string? Type { get; set; }

    public int? OrderId { get; set; }

    public virtual Order? Order { get; set; }
}
