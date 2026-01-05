using cursos.Domain.Enum;

namespace cursos.Application.DTOs;

// Info requested in Register:
public class RegisterDto
{
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    // public Role Role { get; set; } = Role.User;
    public int CourseId { get; set; }
    
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}


// Response once you has registered:
public class UserRegisterResponseDto
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    public string LastName { get; set; }
    
    
    public string Email { get; set; }
    public Role Role { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}


// Response when you login:
public class UserAuthResponseDto
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    
    public Role Role { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}


// Login
public class LoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}


// To Refresh
public class RefreshDto
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}


// To Revoke
public class RevokeTokenDto
{
    public string Email { get; set; }
}