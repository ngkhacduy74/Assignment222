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

        public List<Room> getRoomSlot1()
        {
            var availableRooms = _context.Rooms
                .Where(room => !_context.RoomSchedules
                    .Any(rs => rs.RoomId == room.RoomId && rs.SlotId == 1 && rs.IsBooked == true))
                .ToList();

            return availableRooms;
        }

        public List<Room> getRoomSlot2()
        {
            var availableRooms = _context.Rooms
                .Where(room => !_context.RoomSchedules
                    .Any(rs => rs.RoomId == room.RoomId && rs.SlotId == 2 && rs.IsBooked == true))
                .ToList();

            return availableRooms;
        }

        public List<Room> getRoomSlot3()
        {
            var availableRooms = _context.Rooms
                .Where(room => !_context.RoomSchedules
                    .Any(rs => rs.RoomId == room.RoomId && rs.SlotId == 3 && rs.IsBooked == true))
                .ToList();

            return availableRooms;
        }

        public List<Room> getRoomSlot4()
        {
            var availableRooms = _context.Rooms
                .Where(room => !_context.RoomSchedules
                    .Any(rs => rs.RoomId == room.RoomId && rs.SlotId == 4 && rs.IsBooked == true))
                .ToList();

            return availableRooms;
        }

        public List<Room> getRoomSlot5()
        {
            var availableRooms = _context.Rooms
                .Where(room => !_context.RoomSchedules
                    .Any(rs => rs.RoomId == room.RoomId && rs.SlotId == 5 && rs.IsBooked == true))
                .ToList();

            return availableRooms;
        }

        public Room GetRoomById(int id)
        {
            return _context.Rooms.FirstOrDefault(n => n.RoomId == id);
        }
    }
}