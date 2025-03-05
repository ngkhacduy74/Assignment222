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

        public User getUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(n => n.Email == email);
        }

        public List<User> getAllUser()
        {
            return _context.Users
                    .OrderByDescending(n => n.IsActive)
                    .ThenBy(n => n.UserId)
                    .ToList();
        }

        public bool deleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(n => n.UserId == id);
            if (user == null)
            {
                return false;
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }
    }
}