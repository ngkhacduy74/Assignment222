using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment.Areas.Home.Pages
{
    public class GalleryModel : PageModel
    {
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
            // Handle GET request logic here
            return Page();
        }

        public IActionResult OnPost()
        {
            return new JsonResult(new { success = false, message = "Invalid data." });
        }
    }
}