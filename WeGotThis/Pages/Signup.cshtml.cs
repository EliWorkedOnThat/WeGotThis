using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeGotThis.Data;
using WeGotThis.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

    public IActionResult OnPost()
    {
        var existingMember = _context.Members.FirstOrDefault(m => m.Username == Username);

        if (existingMember != null)
        {
            ModelState.AddModelError("Username", "That username is already taken.");
            return Page();
        }

        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Goal))
        {
            ModelState.AddModelError(string.Empty, "None of the fields can be empty.");
            return Page();
        }

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