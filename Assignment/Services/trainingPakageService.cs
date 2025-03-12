using Assignment.Model;

namespace Assignment.Services
{
    public class trainingPakageService
    {
        private readonly PrivateGymDbContext _context;

        public trainingPakageService(PrivateGymDbContext context)
        {
            _context = context;
        }

        public List<TrainingPackage> get3TrainingPackage()
        {
            return _context.TrainingPackages.Where(n => n.PackageId >= 2).ToList();
        }
    }
}