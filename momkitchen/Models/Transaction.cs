using System;
using System.Collections.Generic;

namespace momkitchen.Models;

public partial class Transaction
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public bool? Status { get; set; }

    public decimal? Amount { get; set; }

    public int? PaymentId { get; set; }

    public int? WalletId { get; set; }

    public virtual Wallet? Wallet { get; set; }
}
