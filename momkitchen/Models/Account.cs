using System;
using System.Collections.Generic;

namespace momkitchen.Models;

public partial class Account
{
    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int RoleId { get; set; }

    public string AccountStatus { get; set; } = null!;

    public virtual ICollection<Chef> Chefs { get; set; } = new List<Chef>();

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Shipper> Shippers { get; set; } = new List<Shipper>();
}
