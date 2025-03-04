using Assignment.Models;

namespace Assignment.Services
{
    public class userService
    {
        private readonly PrivateGymDbContext _context;
        public userService(PrivateGymDbContext context)
        {
            _context = context;

        }
        public User getUserByEmail(string email) {
            return _context.Users.FirstOrDefault(n => n.Email == email);
        
        }
    }
}
