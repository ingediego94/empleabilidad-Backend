using cursos.Application.DTOs;
using cursos.Domain.Entities;
using cursos.Domain.Enum;

namespace cursos.Application.Interfaces;

public interface ICourseService
{
    Task<ResponseCourseDto> CreateAsync(CourseCreateDto dto);
    Task<IEnumerable<ResponseCourseDto>> GetAllAsync();
    Task<ResponseCourseDto> GetByIdAsync(int id);
    Task<ResponseCourseDto> UpdateAsync(int id, CourseUpdateDto dto);
    Task<bool> DeleteAsync(int id);

    Task<bool> PublishAsync(int id);
    Task<bool> UnpublishAsync(int id);
    Task<CourseSummaryDto> GetSummaryAsync(int id);
    Task<PagedResult<ResponseCourseDto>> SearchAsync(
        string? q,
        Status? status,
        int page,
        int pageSize);
}