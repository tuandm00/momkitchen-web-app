using System;
using System.Collections.Generic;

namespace momkitchen.Models;

public partial class Shipper
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Phone { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public int? BatchId { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<Batch> Batches { get; set; } = new List<Batch>();

    public virtual Account? EmailNavigation { get; set; }

    public virtual ICollection<SessionShipper> SessionShippers { get; set; } = new List<SessionShipper>();
}
