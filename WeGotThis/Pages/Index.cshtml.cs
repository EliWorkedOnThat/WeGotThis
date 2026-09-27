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

    public HashSet<int> CompletedByMeIds { get; set; } = new();
    public HashSet<int> RejectedByMeIds { get; set; } = new();

    public void OnGet()
{
    var today = DateTime.UtcNow.Date;

    var seed = today.Year * 10000 + today.Month * 100 + today.Day;
    var random = new Random(seed);

    RandomGoals = _context.Goals
        .Include(g => g.Member)
        .ToList()
        .OrderBy(g => random.Next())
        .Take(5)
        .ToList();

    TotalGoals = _context.Goals.Count();
    CompletedGoals = _context.GoalActions.Count(a => a.IsCompleted);
    RejectedGoals = _context.GoalActions.Count(a => a.IsRejected);

    var memberId = GetCurrentMemberId();
    var since = DateTime.UtcNow.AddHours(-24);

    var myRecentActions = _context.GoalActions
        .Where(a => a.MemberId == memberId && a.ActedAt >= since)
        .ToList();

    CompletedByMeIds = myRecentActions.Where(a => a.IsCompleted).Select(a => a.GoalId).ToHashSet();
    RejectedByMeIds = myRecentActions.Where(a => a.IsRejected).Select(a => a.GoalId).ToHashSet();
}

    public IActionResult OnPostComplete(int id)
    {
        _context.GoalActions.Add(new GoalAction
        {
            GoalId = id,
            MemberId = GetCurrentMemberId(),
            IsCompleted = true
        });
        _context.SaveChanges();

        return RedirectToPage();
    }

    public IActionResult OnPostReject(int id)
    {
        _context.GoalActions.Add(new GoalAction
        {
            GoalId = id,
            MemberId = GetCurrentMemberId(),
            IsRejected = true
        });
        _context.SaveChanges();

        return RedirectToPage();
    }

    private int GetCurrentMemberId()
    {
        var claim = User.FindFirst("MemberId")?.Value;
        return claim != null ? int.Parse(claim) : 0;
    }
}