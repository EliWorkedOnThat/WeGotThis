using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WeGotThis.Data;
using WeGotThis.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
        options.SlidingExpiration = true;
    });

builder.Services.AddRazorPages();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!context.Pool.Any())
    {
        context.Pool.Add(new Pool { TotalGoals = 0 });
        context.SaveChanges();
    }

    if (app.Environment.IsDevelopment() && !context.Members.Any(m => m.Username == "Test1"))
    {
        var hasher = new PasswordHasher<Member>();

        for (int i = 1; i <= 10; i++)
        {
            var name = $"Test{i}";

            var member = new Member { Username = name };
            member.PasswordHash = hasher.HashPassword(member, name);

            member.Goals.Add(new Goal
            {
                Goals = name,
                CreatedAt = DateTime.UtcNow.AddDays(-1)
            });

            context.Members.Add(member);
        }

        context.Pool.Single().TotalGoals += 10;
        context.SaveChanges();
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();