using System;
using System.Collections.Generic;

namespace Assignment.Models;

public partial class Room
{
    public int RoomId { get; set; }

    public string RoomName { get; set; } = null!;

    public int Capacity { get; set; }

    public bool? IsAvailable { get; set; }

    public string? Description { get; set; }

    public string? Location { get; set; }

    public string? Img { get; set; }

    public virtual ICollection<RoomBooking> RoomBookings { get; set; } = new List<RoomBooking>();

    public virtual ICollection<RoomSchedule> RoomSchedules { get; set; } = new List<RoomSchedule>();
}
