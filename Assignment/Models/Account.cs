using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Assignment.Models;

public partial class Account
{
    [BindProperty]
    public string Email { get; set; } = null!;
    [BindProperty]
    public string? Password { get; set; }

    public virtual PersonalTrainer? PersonalTrainer { get; set; }

    public virtual User? User { get; set; }
}
