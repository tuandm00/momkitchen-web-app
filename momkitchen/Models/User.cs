﻿using System;
using System.Collections.Generic;

namespace momkitchen.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public int? RoleId { get; set; }

    public virtual ICollection<Role> Roles { get; } = new List<Role>();
}
