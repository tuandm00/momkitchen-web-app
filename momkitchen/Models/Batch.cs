using System;
using System.Collections.Generic;

namespace momkitchen.Models;

public partial class Batch
{
    public int Id { get; set; }

    public int? ShipperId { get; set; }

    public bool? Status { get; set; }

    public int? SessionId { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Session? Session { get; set; }

    public virtual Shipper? Shipper { get; set; }
}
