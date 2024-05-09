using System.Configuration;
using Album.Mail;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using razorweb.models;

namespace Razorweb;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();

        //Doc chuoi ket noi
        builder.Services.AddDbContext<MyBlogContext>(options =>
        {
            string connectString = builder.Configuration.GetConnectionString("MyBlogContext");
            options.UseSqlServer(connectString);
        });

        builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<MyBlogContext>();

        // Đăng ký AppDbContext
        builder.Services.AddDbContext<MyBlogContext>(options =>
        {
            // Đọc chuỗi kết nối
            string connectstring = builder.Configuration.GetConnectionString("MyBlogContext");
            // Sử dụng MS SQL Server
            options.UseSqlServer(connectstring);
        });

        // Truy cập IdentityOptions
        builder.Services.Configure<IdentityOptions>(options =>
        {
            // Thiết lập về Password
            options.Password.RequireDigit = false; // Không bắt phải có số
            options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
            options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
            options.Password.RequireUppercase = false; // Không bắt buộc chữ in
            options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
            options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

            // Cấu hình Lockout - khóa user
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
            options.Lockout.MaxFailedAccessAttempts = 3; // Thất bại 5 lầ thì khóa
            options.Lockout.AllowedForNewUsers = true;

            // Cấu hình về User.
            options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = true;  // Email là duy nhất

            // Cấu hình đăng nhập.
            options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
            options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại
            options.SignIn.RequireConfirmedAccount = true;

        });

        builder.Services.ConfigureApplicationCookie(options => {
            options.LoginPath = "/login";
            options.LogoutPath = "/logout";
            options.AccessDeniedPath = "/pagedenied";
        });

        builder.Services.AddOptions();                                        // Kích hoạt Options
        var mailsettings = builder.Configuration.GetSection("MailSettings");  // đọc config
        builder.Services.Configure<MailSettings>(mailsettings);               // đăng ký để Inject

        builder.Services.AddTransient<IEmailSender, SendMailService>();        // Đăng ký dịch vụ Mail

        builder.Services.AddRazorPages();



        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();   // Phục hồi thông tin đăng nhập (xác thực)
        app.UseAuthorization();   // Phục hồi thông tinn về quyền của User

        app.MapRazorPages();

        app.Run();
    }
}
