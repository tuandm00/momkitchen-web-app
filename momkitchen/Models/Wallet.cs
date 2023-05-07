using System;
using System.Collections.Generic;

namespace momkitchen.Models;

public partial class Wallet
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<Transaction> Transactions { get; } = new List<Transaction>();
}
