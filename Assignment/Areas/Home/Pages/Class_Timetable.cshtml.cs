using Assignment.Models;

using Assignment.Models;

using Assignment.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment.Areas.Home.Pages
{
    public class Class_TimetableModel : PageModel
    {
        private readonly roomService _roomService;
        public List<Room> listRoom { get; set; }

        public Class_TimetableModel(roomService roomService)
        {
            _roomService = roomService;
        }

        public void OnGet()

        {
            listRoom = _roomService.GetRoomAvailable();
        }
    }
}