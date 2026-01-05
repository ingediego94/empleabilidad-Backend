using cursos.Domain.Enum;

namespace cursos.Domain.Entities;

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public Status Status { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    // Inverse Relations:
    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}