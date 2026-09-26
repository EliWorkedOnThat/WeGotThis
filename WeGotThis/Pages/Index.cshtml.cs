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

    public List<Member> Members { get; set; } = new();
    public int TotalGoals { get; set; }

    public void OnGet()
    {
        Members = _context.Members.Include(m => m.Goal).ToList();
        TotalGoals = _context.Goals.Count();
    }
}