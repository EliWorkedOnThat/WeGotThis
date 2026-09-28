namespace WeGotThis.Models;

public class Member
{
    public int Id { get; set; }
    public string Username { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    
    public int Tokens { get; set; } = 1;

    public List<Goal> Goals { get; set; } = new();
}