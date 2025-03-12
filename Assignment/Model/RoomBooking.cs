using System;
using System.Collections.Generic;

namespace Assignment.Model;

public partial class RoomBooking
{
    public int BookingId { get; set; }

    public int UserId { get; set; }

    public string? PtEmail { get; set; }

    public int RoomId { get; set; }

    public DateOnly TrainingDate { get; set; }

    public TimeOnly StartTime { get; set; }

    public int Duration { get; set; }

    public string? Status { get; set; }

    public virtual PersonalTrainer? PtEmailNavigation { get; set; }

    public virtual Room Room { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
