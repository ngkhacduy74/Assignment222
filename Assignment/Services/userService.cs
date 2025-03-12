using Assignment.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public void createUser(string Fullname, string Email, string Phone, string Password, string Address, DateTime DateOfBirth, int RoleId)
        {
        }

        public List<User> getAllUser()
        {
            return _context.Users?.OrderByDescending(n => n.IsActive)
                                  .ThenBy(n => n.UserId)
                                  .ToList() ?? new List<User>();
        }

        public async Task<bool> createUser(User user)
        {
            if (user == null)
            {
                return false;
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> getUserById(int id)
        {
            return await _context.Users.FindAsync(id);
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