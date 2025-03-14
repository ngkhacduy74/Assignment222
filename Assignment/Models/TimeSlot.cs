using System;
using System.Collections.Generic;

namespace Assignment.Models;

public partial class TimeSlot
{
    public int SlotId { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public virtual ICollection<RoomSchedule> RoomSchedules { get; set; } = new List<RoomSchedule>();

    public virtual ICollection<TrainerAvailability> TrainerAvailabilities { get; set; } = new List<TrainerAvailability>();
}
