using System;
using System.Collections.Generic;

namespace momkitchen.Models;

public partial class Order
{
    public int Id { get; set; }

    public DateTime? Date { get; set; }

    public int? CustomerId { get; set; }

    public int? BatchId { get; set; }

    public string? Status { get; set; }

    public string? DeliveryStatus { get; set; }

    public int? BuildingId { get; set; }

    public int? Quantity { get; set; }

    public int? SessionId { get; set; }

    public string? Email { get; set; }

    public string? CustomerPhone { get; set; }

    public DateTime? DeliveryTime { get; set; }

    public string? Note { get; set; }

    public int? TotalPrice { get; set; }

    public virtual Batch? Batch { get; set; }

    public virtual Building? Building { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Session? Session { get; set; }
}
