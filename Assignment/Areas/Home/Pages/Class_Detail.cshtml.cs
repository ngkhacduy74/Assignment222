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

        public void OnGet()
        {
            if (!string.IsNullOrEmpty(roomId))
            {
                Room = _roomService.GetRoomById(int.Parse(roomId));
            }

            listPT = _ptService.GetAllPersonalTrainer() ?? new List<PersonalTrainer>();
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(roomId))
            {
                return Page();
            }

            return RedirectToPage("/Class_Detail", new { area = "Home", roomId = roomId });
        }
    }
}