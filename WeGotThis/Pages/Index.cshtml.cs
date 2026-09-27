using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WeGotThis.Data;
using WeGotThis.Models;

namespace WeGotThis.Pages;

[Authorize]
public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public List<Goal> RandomGoals { get; set; } = new();
    public int TotalGoals { get; set; }

public int CompletedGoals { get; set; }
public int RejectedGoals { get; set; }

public IActionResult OnPostComplete(int id)
{
    var goal = _context.Goals.Find(id);

    if (goal != null)
    {
        goal.IsCompleted = true;
        _context.SaveChanges();
    }

    return RedirectToPage();
}

public IActionResult OnPostReject(int id)
{
    var goal = _context.Goals.Find(id);

    if (goal != null)
    {
        goal.IsRejected = true;
        _context.SaveChanges();
    }

    return RedirectToPage();
}

public void OnGet()
{
    var timezoneId = Request.Cookies["timezone"] ?? "UTC";

    TimeZoneInfo timezone;
    try
    {
        timezone = TimeZoneInfo.FindSystemTimeZoneById(timezoneId);
    }
    catch (TimeZoneNotFoundException)
    {
        timezone = TimeZoneInfo.Utc;
    }

    var localNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timezone);
    var today = localNow.Date;

    var seed = today.Year * 10000 + today.Month * 100 + today.Day;
    var random = new Random(seed);

    RandomGoals = _context.Goals
        .Include(g => g.Member)
        .ToList()
        .OrderBy(g => random.Next())
        .Take(5)
        .ToList();

    TotalGoals = _context.Goals.Count();
    CompletedGoals = _context.Goals.Count(g => g.IsCompleted);
    RejectedGoals = _context.Goals.Count(g => g.IsRejected);
}
}