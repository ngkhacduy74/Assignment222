using Assignment.Models;

namespace Assignment.Services
{
    public class roomScheduleService
    {
        private readonly PrivateGymDbContext _context;

        public roomScheduleService(PrivateGymDbContext context)
        {
            _context = context;
        }

        public void AddRoomSchedule(RoomSchedule roomSchedule)
        {
            // Check if the user has already booked the room at the same time
            var existingBooking = _context.RoomSchedules
                                          .FirstOrDefault(rs => rs.UserId == roomSchedule.UserId &&
                                                               rs.RoomId == roomSchedule.RoomId &&
                                                               rs.TrainingDate == roomSchedule.TrainingDate &&
                                                               rs.SlotId == roomSchedule.SlotId);

            if (existingBooking != null)
            {
                throw new InvalidOperationException("You have already booked this room at the selected time.");
            }

            _context.RoomSchedules.Add(roomSchedule);
            _context.SaveChanges();
        }

        public RoomSchedule GetRoomScheduleByUserId(int userId)
        {
            return _context.RoomSchedules
                           .FirstOrDefault(rs => rs.UserId == userId && rs.IsBooked == true);
        }
    }
}