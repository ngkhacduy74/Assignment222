using Assignment.Model;

namespace Assignment.Services
{
    public class roleService
    {
        private readonly PrivateGymDbContext _context;

        public roleService(PrivateGymDbContext context)
        {
            _context = context;
        }

        public List<Role> getAllRole()
        {
            return _context.Roles.ToList();
        }
    }
}