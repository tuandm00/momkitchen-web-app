using System;
using System.Collections.Generic;

namespace momkitchen.Models;

public partial class SessionShipper
{
    public int SessionId { get; set; }

    public int? ShipperId { get; set; }

    public virtual Session? Session { get; set; }

    public virtual Shipper? Shipper { get; set; }
}
