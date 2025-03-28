using Assignment.Models;

namespace Assignment.Services
{
    public class TrainerAvailableService
    {
        private readonly PrivateGymDbContext _context;

        public TrainerAvailableService(PrivateGymDbContext context)
        {
            _context = context;
        }

        public void AddTrainerAvailable(TrainerAvailability a)
        {
            _context.TrainerAvailabilities.Add(a);
            _context.SaveChanges();
        }
    }
}