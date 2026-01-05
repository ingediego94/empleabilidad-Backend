using cursos.Application.DTOs;
using cursos.Domain.Entities;

namespace cursos.Application.Interfaces;

public interface ILessonService
{
    Task<IEnumerable<Lesson>> GetAllAsync();
    Task<Lesson?> GetByIdAsync(int id);
    Task<ResponseLessonDto> CreateAsync(LessonCreateDto dto);
    Task<ResponseLessonDto?> UpdateAsync(int id, LessonUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}