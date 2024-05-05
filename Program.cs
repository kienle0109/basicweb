using System.Configuration;
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
        builder.Services.AddDbContext<MyBlogContext>(options => {
            string connectString = builder.Configuration.GetConnectionString("MyBlogContext");
            options.UseSqlServer(connectString);
        });

        

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

        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }
}
