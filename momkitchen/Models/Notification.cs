using System;
using System.Collections.Generic;

namespace momkitchen.Models;

public partial class Notification
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public int? Status { get; set; }

    public string? Content { get; set; }
}
