using cursos.Domain.Enum;

namespace cursos.Application.DTOs;


// User Create:
public class UserCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    // public Role Role { get; set; } = Role.User;
    public int CourseId { get; set; }
}


// User Update:
public class UserUpdateDto
{
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int CourseId { get; set; }
}



// User Response:
public class ResponseUserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Role Role { get; set; }
    public int CourseId { get; set; }
}