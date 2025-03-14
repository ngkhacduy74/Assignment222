using Assignment.Models;

namespace Assignment.Services
{
    public class timeSlotsService
    {
        private readonly PrivateGymDbContext _context;

        public timeSlotsService(PrivateGymDbContext context)
        {
            _context = context;
        }

        public List<TimeSlot> getAllTimeSlot()
        {
            return _context.TimeSlots.ToList();
        }
    }
}