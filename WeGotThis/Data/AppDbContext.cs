using Microsoft.EntityFrameworkCore;
using WeGotThis.Models;

namespace WeGotThis.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Pool> Pool => Set<Pool>();

    public DbSet<Member> Members => Set<Member>();
    public DbSet<Goal> Goals => Set<Goal>();

    public DbSet<GoalAction> GoalActions => Set<GoalAction>();

    public DbSet<Quote> Quotes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Quote>().HasData(
            new Quote { Id = 1,  Text = "You have power over your mind, not outside events. Realize this, and you will find strength.", Author = "Marcus Aurelius" },
            new Quote { Id = 2,  Text = "The impediment to action advances action. What stands in the way becomes the way.", Author = "Marcus Aurelius" },
            new Quote { Id = 3,  Text = "We suffer more often in imagination than in reality.", Author = "Seneca" },
            new Quote { Id = 4,  Text = "Luck is what happens when preparation meets opportunity.", Author = "Seneca" },
            new Quote { Id = 5,  Text = "It's not what happens to you, but how you react to it that matters.", Author = "Epictetus" },
            new Quote { Id = 6,  Text = "No person is free who is not master of themselves.", Author = "Epictetus" },
            new Quote { Id = 7,  Text = "Waste no more time arguing what a good person should be. Be one.", Author = "Marcus Aurelius" },
            new Quote { Id = 8,  Text = "He who fears death will never do anything worthy of a living person.", Author = "Seneca" },
            new Quote { Id = 9,  Text = "First say to yourself what you would be, then do what you have to do.", Author = "Epictetus" },
            new Quote { Id = 10, Text = "The best revenge is not to be like your enemy.", Author = "Marcus Aurelius" }
        );
    }

}