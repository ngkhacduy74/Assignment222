using System;
using System.Collections.Generic;

namespace Assignment.Models;

public partial class TrainerAvailability
{
    public int AvailabilityId { get; set; }

    public string PT_Email { get; set; } = null!;

    public DateOnly TrainingDate { get; set; }

    public int SlotId { get; set; }

    public bool? IsAvailable { get; set; }

    public virtual PersonalTrainer PtEmailNavigation { get; set; } = null!;

    public virtual TimeSlot Slot { get; set; } = null!;
}
