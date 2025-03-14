using Assignment.Models;
using Assignment.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Assignment.Areas.Auth
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public bool RememberMe { get; set; }

        private readonly authService _authService;
        private readonly userService _userService;
        private readonly ISession _session;

        public LoginModel(authService authService, userService userService, IHttpContextAccessor httpContextAccessor)
        {
            _authService = authService;
            _userService = userService;
            _session = httpContextAccessor.HttpContext.Session;
        }

        public void OnGet()
        {
            Debug.WriteLine("Trang đăng nhập được tải.");
        }

        public IActionResult OnPost()
        {
            Debug.WriteLine("Đăng nhập được gọi.");
            var account = _authService.Authenticate(Email, Password);

            if (account == null)
            {
                _session.SetString("error", "Sai tên đăng nhập hoặc mật khẩu");
                return RedirectToPage("/Login");
            }

            var user = _userService.getUserByEmail(account.Email);

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            string userJson = JsonSerializer.Serialize(user, options);
            HttpContext.Session.SetString("success", userJson);

            return RedirectToPage("/Home", new { area = "Home" });
        }
    }
}