using System;
using System.Collections.Generic;

namespace Assignment.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? Fullname { get; set; }

    public string? Phone { get; set; }

    public string Email { get; set; } = null!;

    public string? Address { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public int? RoleId { get; set; }

    public int? PackageId { get; set; }

    public bool? IsActive { get; set; }

    public int? ServiceId { get; set; }

    public string? Img { get; set; }

    public virtual Account EmailNavigation { get; set; } = null!;

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual TrainingPackage? Package { get; set; }

    public virtual Role? Role { get; set; }

    public virtual ICollection<RoomBooking> RoomBookings { get; set; } = new List<RoomBooking>();

    public virtual Service? Service { get; set; }

    public virtual ICollection<TrainingSchedule> TrainingSchedules { get; set; } = new List<TrainingSchedule>();

    public virtual ICollection<UserPtBooking> UserPtBookings { get; set; } = new List<UserPtBooking>();
}
