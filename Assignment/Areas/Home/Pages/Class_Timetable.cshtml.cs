using Assignment.Model;
using Assignment.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment.Areas.Home.Pages
{
    public class Class_TimetableModel : PageModel
    {
        private readonly roomService _roomService;
        public List<Room> listRoom { get; set; }

        [BindProperty]
        public string roomId { get; set; }

        public Class_TimetableModel(roomService roomService)
        {
            _roomService = roomService;
        }

        public void OnGet()

        {
            listRoom = _roomService.GetRoomAvailable();
        }

        public IActionResult OnPost()
        {
            var room = _roomService.GetRoomById(int.Parse(roomId));
            return RedirectToPage("/Class_Detail", new { area = "Home", roomId = roomId });
        }
    }
}