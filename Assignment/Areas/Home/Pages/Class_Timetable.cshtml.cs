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
        public List<Room> listRoom1 { get; set; }
        public List<Room> listRoom2 { get; set; }
        public List<Room> listRoom3 { get; set; }
        public List<Room> listRoom4 { get; set; }
        public List<Room> listRoom5 { get; set; }

        public Class_TimetableModel(roomService roomService)
        {
            _roomService = roomService;
        }

        public void OnGet()

        {
            listRoom1 = _roomService.getRoomSlot1();
            listRoom2 = _roomService.getRoomSlot2();
            listRoom3 = _roomService.getRoomSlot3();
            listRoom4 = _roomService.getRoomSlot4();
            listRoom5 = _roomService.getRoomSlot5();
        }
    }
}