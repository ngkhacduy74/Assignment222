using System;
using System.Collections.Generic;

namespace Assignment.Models;

public partial class TrainingPackage
{
    public int PackageId { get; set; }

    public string? PackageType { get; set; }

    public decimal? Price { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
