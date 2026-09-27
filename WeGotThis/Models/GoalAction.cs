namespace WeGotThis.Models;

public class GoalAction
{
    public int Id { get; set; }

    public int GoalId { get; set; }
    public Goal? Goal { get; set; }

    public int MemberId { get; set; }
    public Member? Member { get; set; }

    public bool IsCompleted { get; set; }
    public bool IsRejected { get; set; }

    public DateTime ActedAt { get; set; } = DateTime.UtcNow;
}