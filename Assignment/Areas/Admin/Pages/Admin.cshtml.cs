using Assignment.Hubs;
using Assignment.Models;
using Assignment.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace Assignment.Areas.Admin.Pages
{
    public class AdminModel : PageModel
    {
        private readonly userService _userService;
        private readonly roleService _roleService;
        private readonly IHubContext<UserHub> _hubContext;

        //private readonly ISession _session;
        public List<User> list { get; set; }

        public List<Role> listRole { get; set; }

        public AdminModel(userService userService, roleService roleService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _roleService = roleService;
            //_session = httpContextAccessor.HttpContext.Session;
        }

        public void OnGet()
        {
            list = _userService.getAllUser();
            listRole = _roleService.getAllRole();
        }

        public async Task<IActionResult> OnPostDeleteUser(int id)
        {
            bool isDelete = _userService.deleteUser(id);
            if (!isDelete)
            {
                return NotFound(new { message = "User not found" });
            }
            await _hubContext.Clients.All.SendAsync("UserDeleted", id);
            return new JsonResult(new { success = true, message = "User deleted successfully", id });
        }
    }
}