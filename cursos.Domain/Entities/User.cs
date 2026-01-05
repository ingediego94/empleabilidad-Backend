using cursos.Domain.Enum;

namespace cursos.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public Role Role { get; set; } = Role.User;
    public int CourseId { get; set; }
    
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpire { get; set; }
    
    public DateTime CreatedAt { get; set; } 
    public DateTime UpdatedAt { get; set; }
    
    // Relations:
    public Course Course { get; set; }
}