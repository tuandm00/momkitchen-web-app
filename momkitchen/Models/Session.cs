using System;
using System.Collections.Generic;

namespace momkitchen.Models;

public partial class Session
{
    public int Id { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public bool? Status { get; set; }

    public string? Title { get; set; }

    public virtual ICollection<Batch> Batches { get; set; } = new List<Batch>();

    public virtual ICollection<FoodPackageInSession> FoodPackageInSessions { get; set; } = new List<FoodPackageInSession>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<SessionShipper> SessionShippers { get; set; } = new List<SessionShipper>();
}
