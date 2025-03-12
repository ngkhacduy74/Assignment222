using Assignment.Model;

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
    }
}