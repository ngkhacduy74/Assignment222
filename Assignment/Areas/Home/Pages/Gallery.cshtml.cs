using Assignment.Models;
using Assignment.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment.Areas.Home.Pages
{
    public class GalleryModel : PageModel
    {
        private readonly TrainerAvailableService _trainerAvailableService;
        private readonly roomScheduleService _roomScheduleService;
        private readonly emailService _emailService;
        private readonly userService _userService;

        public GalleryModel(TrainerAvailableService trainerAvailableService, roomScheduleService roomScheduleService, emailService emailService, userService userService)
        {
            _trainerAvailableService = trainerAvailableService;
            _roomScheduleService = roomScheduleService;
            _userService = userService;
            _emailService = emailService;
        }

        [BindProperty]
        public int UserId { get; set; }

        [BindProperty]
        public int RoomId { get; set; }

        [BindProperty]
        public int TimeSlotId { get; set; }

        [BindProperty]
        public string PtEmail { get; set; }

        [BindProperty]
        public DateTime TrainingDate { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (UserId == null || UserId == 0)
            {
                return RedirectToPage("/Login", new { area = "Auth" });
            }
            var existingBooking = _roomScheduleService.GetRoomScheduleByUserId(UserId);

            if (existingBooking != null)
            {
                ModelState.AddModelError("", "You have already booked a room.");
                return RedirectToPage("/Class-details", new { area = "Home" });
            }
            var a = new TrainerAvailability
            {
                AvailabilityId = GenerateRandomNumber(),
                PT_Email = PtEmail,
                TrainingDate = DateOnly.FromDateTime(DateTime.Today),
                SlotId = TimeSlotId,
                IsAvailable = true
            };
            var b = new RoomSchedule
            {
                UserId = UserId,
                ScheduleId = GenerateRandomNumber(),
                RoomId = RoomId,
                TrainingDate = DateOnly.FromDateTime(DateTime.Today),
                SlotId = TimeSlotId,
                IsBooked = true
            };
            _trainerAvailableService.AddTrainerAvailable(a);
            _roomScheduleService.AddRoomSchedule(b);

            return new JsonResult(new { success = true, message = "Booking successful. Confirmation email sent." });
        }

        public int GenerateRandomNumber()
        {
            Random random = new Random();
            return random.Next(0, 20001);
        }
    }
}