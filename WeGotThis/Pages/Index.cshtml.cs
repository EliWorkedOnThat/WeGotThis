using Microsoft.AspNetCore.Authorization;
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

    public void OnGet()
    {
        RandomGoals = _context.Goals
            .Include(g => g.Member)
            .ToList()
            .OrderBy(g => Guid.NewGuid())
            .Take(5)
            .ToList();

        TotalGoals = _context.Goals.Count();
    }
}