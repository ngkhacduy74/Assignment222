using System;
using System.Collections.Generic;

namespace Assignment.Model;

public partial class Account
{
    public string Email { get; set; } = null!;

    public string? Password { get; set; }

    public virtual PersonalTrainer? PersonalTrainer { get; set; }

    public virtual User? User { get; set; }
}
