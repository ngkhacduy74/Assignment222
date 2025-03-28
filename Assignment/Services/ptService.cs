using Assignment.Models;

namespace Assignment.Services
{
    public class ptService
    {
        private readonly PrivateGymDbContext _context;

        public ptService(PrivateGymDbContext context)
        {
            _context = context;
        }

        public List<PersonalTrainer> GetAllPersonalTrainer()
        {
            return _context.PersonalTrainers.ToList();
        }

        public List<PersonalTrainer> GetPersonalTrainersIsAvailableOnTime(int timeslotId)
        {
            var availablePTEmails = _context.TrainerAvailabilities
                .Where(ta => ta.SlotId == timeslotId && ta.IsAvailable == true)
                .Select(ta => ta.PT_Email)  // Chọn chỉ có Email của PT
                .ToList();

            var availablePersonalTrainers = _context.PersonalTrainers
                .Where(pt => !availablePTEmails.Contains(pt.Email))
                .ToList();

            return availablePersonalTrainers;
        }
    }
}