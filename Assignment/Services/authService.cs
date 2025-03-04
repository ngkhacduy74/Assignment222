using Assignment.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Services
{
    public class authService
    {
        private readonly PrivateGymDbContext _context;
        public authService(PrivateGymDbContext context)
        {
            _context = context;
        }
        public Account Authenticate(string email, string password)
        {
            var account = _context.Accounts.FirstOrDefault(n => n.Email == email);
            if (account == null)
            {
                return null; 
            }

            if (account.Password != password)
            {
                return null;
            }

            return account;
        }

    }
}
