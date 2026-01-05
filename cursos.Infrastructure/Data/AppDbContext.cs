using cursos.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace cursos.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        :base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Users restrictions:
        var user = modelBuilder.Entity<User>();
        user.HasIndex(u => u.Email)
            .IsUnique();
        
        // Lessons restrictions:
        var lesson = modelBuilder.Entity<Lesson>();
        lesson.HasIndex(l => l.Order)
            .IsUnique();
        
        
        base.OnModelCreating(modelBuilder);
    }
    
    // Tables:
    public DbSet<User> Users { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    
}