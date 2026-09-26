using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using WeGotThis.Data;

namespace WeGotThis.Pages;

public class LoginModel : PageModel
{
    private readonly AppDbContext _context;
    private readonly PasswordHasher<Models.Member> _hasher = new();

    public LoginModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public string Username { get; set; } = "";

    [BindProperty]
    public string Password { get; set; } = "";

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        var member = _context.Members.FirstOrDefault(m => m.Username == Username);

        if (member == null)
        {
            ModelState.AddModelError(string.Empty, "Incorrect username or password.");
            return Page();
        }

        var result = _hasher.VerifyHashedPassword(member, member.PasswordHash, Password);

        if (result == PasswordVerificationResult.Failed)
        {
            ModelState.AddModelError(string.Empty, "Incorrect username or password.");
            return Page();
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, member.Username),
            new Claim("MemberId", member.Id.ToString())
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

        return RedirectToPage("Index");
    }
}