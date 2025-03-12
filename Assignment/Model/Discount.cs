using System;
using System.Collections.Generic;

namespace Assignment.Model;

public partial class Discount
{
    public int DiscountId { get; set; }

    public string? DiscountCode { get; set; }

    public decimal? DiscountPercent { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
