namespace Assignment.Models
{
    public class Trainer_Availability
    {
        public int AvailabilityId { get; set; }
        public string PT_Email { get; set; } // FK to PersonalTrainer
        public DateOnly TrainingDate { get; set; }
        public int SlotId { get; set; } // FK to TimeSlots
        public bool IsAvailable { get; set; }

        public virtual PersonalTrainer PT_EmailNavigation { get; set; } // Navigation property
        public virtual TimeSlot Slot { get; set; } // Navigation property
    }
}