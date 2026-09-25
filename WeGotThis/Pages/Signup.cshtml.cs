using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeGotThis.Data;
using WeGotThis.Models;
using Microsoft.AspNetCore.Identity;

namespace WeGotThis.Pages;

public class SignUpModel : PageModel
{

    private readonly AppDbContext _context;
    private readonly PasswordHasher<Member> _hasher = new();

    public SignUpModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public string Username { get; set; } = "";

    [BindProperty]
    public string Password { get; set; } = "";

    [BindProperty]
    public string Goal { get; set; } = "";

    public void OnGet()
    {
    }

    public IActionResult  OnPost()
    {
        var member = new Member
        {
            Username = Username,
        };

        member.PasswordHash = _hasher.HashPassword(member, Password);

        member.Goal = new Goal
        {
          Goals = Goal  
        };

        _context.Members.Add(member);
        _context.SaveChanges();

        return RedirectToPage("Index");
    }
}