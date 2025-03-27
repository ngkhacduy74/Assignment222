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
            return _context.PersonalTrainers
        .Where(pt => _context.TrainerAvailabilities
            .Any(ta => ta.PtEmail == pt.Email && ta.SlotId == timeslotId && ta.IsAvailable == true))
        .ToList();
        }

        // sẽ lấy được id của time slot và sẽ kiểm tra xem trong bảng Room_Schedule thì đã có PT nào được book trong timelsot đó rồi
        // Nếu chưa có ai thì sẽ hiển thị tất cả các PT , còn nếu có rồi thì sẽ trừ đi người đó để hiển thị
    }
}