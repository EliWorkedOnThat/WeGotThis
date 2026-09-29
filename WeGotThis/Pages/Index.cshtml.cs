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
    public int Tokens { get; set; }
    public Quote? DailyQuote { get; set; }

    public HashSet<int> CompletedByMeIds { get; set; } = new();
    public HashSet<int> RejectedByMeIds { get; set; } = new();

    public void OnGet()
    {
        var today = DateTime.UtcNow.Date;

        var seed = today.Year * 10000 + today.Month * 100 + today.Day;
        var random = new Random(seed);

        RandomGoals = _context.Goals
            .Include(g => g.Member)
            .Where(g => g.CreatedAt < today)
            .OrderBy(g => g.Id)
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

        Tokens = _context.Members.Find(memberId)?.Tokens ?? 0;

        DailyQuote = _context.Quotes.OrderBy(q => EF.Functions.Random()).FirstOrDefault();

    }

    public IActionResult OnPostComplete(int id)
    {
        var memberId = GetCurrentMemberId();
        var since = DateTime.UtcNow.AddHours(-24);

        var alreadyActed = _context.GoalActions.Any(a =>
            a.MemberId == memberId && a.GoalId == id && a.ActedAt >= since);

        var member = _context.Members.Find(memberId);

        if (alreadyActed || member == null)
        {
            return RedirectToPage();
        }

        _context.GoalActions.Add(new GoalAction
        {
            GoalId = id,
            MemberId = memberId,
            IsCompleted = true
        });

        member.Tokens += 2;

        _context.SaveChanges();

        return RedirectToPage();
    }

public IActionResult OnPostReject(int id)
    {
        var memberId = GetCurrentMemberId();
        var since = DateTime.UtcNow.AddHours(-24);

        var alreadyActed = _context.GoalActions.Any(a =>
            a.MemberId == memberId && a.GoalId == id && a.ActedAt >= since);

        if (alreadyActed)
        {
            return RedirectToPage();
        }

        _context.GoalActions.Add(new GoalAction
        {
            GoalId = id,
            MemberId = memberId,
            IsRejected = true
        });

        _context.SaveChanges();

        return RedirectToPage();
    }

    public IActionResult OnPostSubmitGoal(string goalText)
    {
        var memberId = GetCurrentMemberId();
        var member = _context.Members.Find(memberId);

        if (member == null || member.Tokens < 1 || string.IsNullOrWhiteSpace(goalText))
        {
            return RedirectToPage();
        }

        member.Tokens--;

        goalText = goalText.Trim();
        if (goalText.Length > 120)
        {
            return RedirectToPage();
        }

        _context.Goals.Add(new Goal
        {
            Goals = goalText,
            MemberId = memberId
        });

        var pool = _context.Pool.Single();
        pool.TotalGoals++;

        _context.SaveChanges();

        return RedirectToPage();
    }

    private int GetCurrentMemberId()
    {
        var claim = User.FindFirst("MemberId")?.Value;
        return claim != null ? int.Parse(claim) : 0;
    }
}