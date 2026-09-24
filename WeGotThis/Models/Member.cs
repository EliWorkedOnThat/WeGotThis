namespace WeGotThis.Models;

public class Member
{
    public int Id { get; set; }
    public string Username { get; set; } = "";
    public string PasswordHash { get; set; } = "";

    public Goal? Goal {get; set;}
}