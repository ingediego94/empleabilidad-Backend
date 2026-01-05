using cursos.Domain.Entities;

namespace cursos.Application.Interfaces;

public interface ILessonService
{
    Task<IEnumerable<Lesson>> GetAllAsync();
    Task<Lesson> GetByIdAsync(int id);
    Task<Lesson> CreateAsync(Lesson lesson);
    Task<bool> UpdateAsync(Lesson lesson);
    Task<bool> DeleteAsync(int id);
}