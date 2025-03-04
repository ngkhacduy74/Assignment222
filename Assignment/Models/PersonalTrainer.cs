using System;
using System.Collections.Generic;

namespace Assignment.Models;

public partial class PersonalTrainer
{
    public string Email { get; set; } = null!;

    public string? Fullname { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public bool? IsActive { get; set; }

    public string? Expertise { get; set; }

    public string? Achievements { get; set; }

    public virtual Account EmailNavigation { get; set; } = null!;

    public virtual ICollection<TrainingSchedule> TrainingSchedules { get; set; } = new List<TrainingSchedule>();

    public virtual ICollection<UserPtBooking> UserPtBookings { get; set; } = new List<UserPtBooking>();
}
