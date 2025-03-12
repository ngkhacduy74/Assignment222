using System;
using System.Collections.Generic;

namespace Assignment.Model;

public partial class Service
{
    public int ServiceId { get; set; }

    public string? ServiceType { get; set; }

    public decimal? Price { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
