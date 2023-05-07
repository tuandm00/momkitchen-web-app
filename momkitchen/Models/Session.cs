using System;
using System.Collections.Generic;

namespace momkitchen.Models;

public partial class Session
{
    public int Id { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Batch> Batches { get; set; } = new List<Batch>();

    public virtual ICollection<FoodPackageInSession> FoodPackageInSessions { get; set; } = new List<FoodPackageInSession>();

    public virtual SessionShipper IdNavigation { get; set; } = null!;
}
