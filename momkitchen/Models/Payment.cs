using System;
using System.Collections.Generic;

namespace momkitchen.Models;

public partial class Payment
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public int OrderId { get; set; }

    public string Status { get; set; } = null!;

    public decimal Amount { get; set; }

    public virtual Order Order { get; set; } = null!;
}
