using cursos.Domain.Enum;

namespace cursos.Application.DTOs;

public class CourseCreateDto
{
    public string Title { get; set; } = string.Empty;
    public Status Status { get; set; }
    public bool IsDeleted { get; set; } = false;
}


public class CourseUpdateDto
{
    public string Title { get; set; } = string.Empty;
    public Status Status { get; set; }
    public bool IsDeleted { get; set; }
}

public class ResponseCourseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public Status Status { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}


public class CourseSummaryDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public Status Status { get; set; }
    public int TotalLessons { get; set; }
    public DateTime LastUpdate { get; set; }
}