using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WeGotThis.Pages;

public class SignUpModel : PageModel
{
    [BindProperty]
    public string Username { get; set; } = "";

    [BindProperty]
    public string Password { get; set; } = "";

    [BindProperty]
    public string Goal { get; set; } = "";

    public void OnGet()
    {
    }

    public void OnPost()
    {
    }
}