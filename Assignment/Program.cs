using Assignment.Models;
using Assignment.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Thêm DbContext với kết nối cơ sở dữ liệu
builder.Services.AddDbContext<PrivateGymDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHttpContextAccessor();
// Thêm Razor Pages và cấu hình các area
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;  
    options.Cookie.IsEssential = true; 
});
builder.Services.AddScoped<authService>();
builder.Services.AddScoped<userService>();
builder.Services.AddScoped<trainingPakageService>();


builder.Services.AddRazorPages(options =>
{
    
    options.Conventions.AddAreaPageRoute("Auth", "/{page}", "/Auth/{page}");
    options.Conventions.AddAreaPageRoute("Home", "/{page}", "/Home/{page}");
});

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles(); 
app.UseSession();      
app.UseRouting();      

app.UseAuthorization();

app.MapRazorPages();   

app.Run(); 
