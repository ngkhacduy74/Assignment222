using System;
using System.Collections.Generic;

namespace Assignment.Model;

public partial class UserPtBooking
{
    public int BookingId { get; set; }

    public int? UserId { get; set; }

    public string? PtEmail { get; set; }

    public DateOnly? BookingDate { get; set; }

    public string? Status { get; set; }

    public virtual PersonalTrainer? PtEmailNavigation { get; set; }

    public virtual ICollection<TrainingSchedule> TrainingSchedules { get; set; } = new List<TrainingSchedule>();

    public virtual User? User { get; set; }
}
