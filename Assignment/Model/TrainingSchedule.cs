using System;
using System.Collections.Generic;

namespace Assignment.Model;

public partial class TrainingSchedule
{
    public int ScheduleId { get; set; }

    public string? UserEmail { get; set; }

    public string? PtEmail { get; set; }

    public DateOnly? TrainingDate { get; set; }

    public TimeOnly? StartTime { get; set; }

    public int? Duration { get; set; }

    public string? Notes { get; set; }

    public int? BookingId { get; set; }

    public virtual UserPtBooking? Booking { get; set; }

    public virtual PersonalTrainer? PtEmailNavigation { get; set; }

    public virtual User? UserEmailNavigation { get; set; }
}
