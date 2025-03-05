using Assignment.Models;
using Assignment.Services;

using Assignment.Hubs; // Thêm namespace UserHub
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Thêm DbContext với kết nối cơ sở dữ liệu
builder.Services.AddDbContext<PrivateGymDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpContextAccessor();

// Thêm SignalR
builder.Services.AddSignalR();

// Cấu hình Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Đăng ký các dịch vụ Scoped
builder.Services.AddScoped<authService>();
builder.Services.AddScoped<userService>();
builder.Services.AddScoped<trainingPakageService>();
builder.Services.AddScoped<roleService>();

// Cấu hình Razor Pages với các area
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AddAreaPageRoute("Auth", "/{page}", "/Auth/{page}");
    options.Conventions.AddAreaPageRoute("Home", "/{page}", "/Home/{page}");
});

// Xây dựng ứng dụng
var app = builder.Build();

// Định tuyến SignalR
//app.MapHub<UserHub>("/userHub");

// Kiểm tra môi trường (Xử lý lỗi trong môi trường Production)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

// Kích hoạt Static Files
app.UseStaticFiles();

// Kích hoạt Session
app.UseSession();

// Định tuyến
app.UseRouting();

// Kích hoạt Authorization (nếu có Authentication, đặt trước `UseAuthorization`)
app.UseAuthorization();

// Định tuyến Razor Pages
app.MapRazorPages();

// Chạy ứng dụng
app.Run();