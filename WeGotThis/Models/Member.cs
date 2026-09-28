namespace WeGotThis.Models;

public class Member
{
    public int Id { get; set; }
    public string Username { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    
    public string SignupIp { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int Tokens { get; set; } = 1;

    public List<Goal> Goals { get; set; } = new();
}