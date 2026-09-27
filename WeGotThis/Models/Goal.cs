namespace WeGotThis.Models;

public class Goal
{
    public int Id { get; set; }
    public string Goals { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsCompleted { get; set;}
    public bool IsRejected { get; set;}

    public int MemberId { get; set; }
    public Member? Member { get; set; }
}