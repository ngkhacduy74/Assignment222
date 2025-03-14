using Assignment.Models;

namespace Assignment.Services
{
    public class roomService
    {
        private readonly PrivateGymDbContext _context;

        public roomService(PrivateGymDbContext context)
        {
            _context = context;
        }

        public List<Room> GetAllRoom()
        {
            return _context.Rooms.ToList();
        }

        public List<Room> GetRoomAvailable()
        {
            return _context.Rooms.Where(n => (bool)n.IsAvailable).ToList();
        }

        public Room GetRoomById(int id)
        {
            return _context.Rooms.FirstOrDefault(n => n.RoomId == id);
        }
    }
}