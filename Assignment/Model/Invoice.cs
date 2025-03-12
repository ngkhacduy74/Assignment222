using System;
using System.Collections.Generic;

namespace Assignment.Model;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    public string? UserPhone { get; set; }

    public DateOnly? PaymentDate { get; set; }

    public int? DiscountId { get; set; }

    public decimal? TotalAmount { get; set; }

    public int? UserId { get; set; }

    public virtual Discount? Discount { get; set; }

    public virtual User? User { get; set; }
}
