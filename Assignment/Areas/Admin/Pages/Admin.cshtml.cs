using Assignment.Hubs;
using Assignment.Models;
using Assignment.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Areas.Admin.Pages
{
    public class AdminModel : PageModel
    {
        private readonly userService _userService;
        private readonly roleService _roleService;
        private readonly accountService _accountService;
        private readonly IHubContext<UserHub> _hubContext;

        public AdminModel(userService userService, roleService roleService,
                   accountService accountService, IHubContext<UserHub> hubContext)
        {
            _userService = userService;
            _roleService = roleService;
            _accountService = accountService;
            _hubContext = hubContext;
        }

        public static int GenerateRandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max + 1);
        }

        public class CreateUserModel
        {
            [Required]
            [BindProperty]
            public string Fullname { get; set; } = string.Empty;

            [Required]
            [BindProperty]
            public string Phone { get; set; } = string.Empty;

            [Required]
            [BindProperty]
            public string Email { get; set; } = string.Empty;

            [Required]
            [BindProperty]
            public string Password { get; set; } = string.Empty;

            [Required]
            [BindProperty]
            public string Re_password { get; set; } = string.Empty;

            [Required]
            [BindProperty]
            public string Address { get; set; } = string.Empty;

            [Required]
            [BindProperty]
            public DateTime? DateOfBirth { get; set; }

            [Required]
            [BindProperty]
            public string Role { get; set; } = string.Empty;
        }

        [BindProperty]
        public CreateUserModel model { get; set; }

        //private readonly ISession _session;

        public List<User> list { get; set; } = new List<User>();
        public List<Role> listRole { get; set; } = new List<Role>();

        public void OnGet()
        {
            list = _userService.getAllUser() ?? new List<User>();
            listRole = _roleService.getAllRole() ?? new List<Role>();
        }

        public async Task<IActionResult> OnGetUserAsync(int id)
        {
            var user = await _userService.getUserById(id);
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            return new JsonResult(user);
        }

        public void OnPost()
        {
            Debug.WriteLine("⚠ OnPost mặc định được gọi!");
        }

        public async Task<IActionResult> OnPostCreateUser()
        {
            bool emailCheck = _accountService.checkEmail(model.Email);
            if (emailCheck)
            {
                return new JsonResult(new { success = false, message = "Email đã tồn tại!" });
            }

            var newAccount = new Account
            {
                Email = model.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password)
            };

            var createAccount = await _accountService.createAccount(newAccount);

            if (!createAccount)
            {
                return new JsonResult(new { success = false, message = "Tạo tài khoản thất bại!" });
            }

            var newUser = new User
            {
                UserId = GenerateRandomNumber(0, 2000000),
                Fullname = model.Fullname,
                Phone = model.Phone,
                Email = model.Email,
                Address = model.Address,
                DateOfBirth = model.DateOfBirth.HasValue ? DateOnly.FromDateTime(model.DateOfBirth.Value) : null,
                RoleId = int.Parse(model.Role),
                IsActive = true
            };

            var createdUser = await _userService.createUser(newUser);
            if (!createdUser)
            {
                return new JsonResult(new { success = false, message = "Tạo người dùng thất bại!" });
            }

            await _hubContext.Clients.All.SendAsync("UserCreated", createdUser);

            list = _userService.getAllUser() ?? new List<User>();

            return new JsonResult(new { success = true, message = "Tạo người dùng thành công!", user = createdUser });
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