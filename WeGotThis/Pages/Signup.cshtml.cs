using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WeGotThis.Pages;

public class SignUpModel : PageModel
{
    [BindProperty]

    public string Username { get; set; } = "";

    public string Password {get; set; } = "";

    public void OnGet()
    {
    }
}