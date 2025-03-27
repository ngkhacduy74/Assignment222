using Assignment.Models;
using Assignment.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment.Areas.Home.Pages
{
    public class Class_DetailModel : PageModel
    {
        private readonly ptService _ptService;
        private readonly roomService _roomService;

        public List<PersonalTrainer> listPT { get; set; } = new List<PersonalTrainer>();

        public Class_DetailModel(ptService ptService, roomService roomService)
        {
            _ptService = ptService;
            _roomService = roomService;
        }

        [BindProperty(SupportsGet = true)] // ✅ Allow roomId to be received via GET request
        public string roomId { get; set; }

        public Room Room { get; set; }

        [BindProperty]
        public string UserId { get; set; }

        [BindProperty]
        public string RoomId { get; set; }

        [BindProperty]
        public string PtEmail { get; set; }

        [BindProperty]
        public int TimeSlotId { get; set; }

        [BindProperty]
        public DateTime TrainingDate { get; set; }

        public class BookTrainerRequest
        {
            public int UserId { get; set; }
            public int RoomId { get; set; }
            public string PtEmail { get; set; }
            public int TimeSlotId { get; set; }
            public string TrainingDate { get; set; }
        }

        public void OnGet()
        {
            if (!string.IsNullOrEmpty(roomId))
            {
                var parts = roomId.Split('-');
                if (parts.Length == 2)
                {
                    int roomIdValue;
                    if (int.TryParse(parts[0], out roomIdValue))
                    {
                        Room = _roomService.GetRoomById(roomIdValue);
                    }

                    int timeSlotId;
                    if (int.TryParse(parts[1], out timeSlotId))
                    {
                        TimeSlotId = timeSlotId;
                    }
                }
            }

            //listPT = _ptService.GetAllPersonalTrainer() ?? new List<PersonalTrainer>();
            listPT = _ptService.GetPersonalTrainersIsAvailableOnTime(TimeSlotId);
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(roomId))
            {
                return Page();
            }
            Console.WriteLine("asssssssssssss");
            return RedirectToPage("/Class_Detail", new { area = "Home", roomId = roomId });
        }

        // Xử lý handler BookTrainer
        public async Task<IActionResult> OnPostBookTrainer([FromBody] BookTrainerRequest request)
        {
            // Xử lý đặt lịch ở đây
            if (ModelState.IsValid)
            {
                // Đặt phòng logic
                // Lưu thông tin vào database hoặc thực hiện thao tác cần thiết
                return new JsonResult(new { success = true, message = "Booking successful!" });
            }
            return new JsonResult(new { success = false, message = "Booking failed." });
        }
    }
}