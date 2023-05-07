﻿using System;
using System.Collections.Generic;

namespace momkitchen.Models;

public partial class FoodPackageStyle
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public int? FoodPackageId { get; set; }

    public virtual FoodPackage? FoodPackage { get; set; }
}