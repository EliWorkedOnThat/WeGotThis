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
}