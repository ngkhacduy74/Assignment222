using System;
using System.Collections.Generic;

namespace Assignment.Models;

public partial class RoomSchedule
{
    public int ScheduleId { get; set; }

    public int RoomId { get; set; }

    public DateOnly TrainingDate { get; set; }

    public int SlotId { get; set; }

    public bool? IsBooked { get; set; }

    public virtual Room Room { get; set; } = null!;

    public virtual TimeSlot Slot { get; set; } = null!;
}
