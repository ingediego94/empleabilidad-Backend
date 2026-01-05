using cursos.Domain.Entities;
using cursos.Domain.Interfaces;

namespace cursos.Application.Services;

public class LessonService
{
    private readonly IGeneralRepository<Lesson> _lessonRepository;

    public LessonService(IGeneralRepository<Lesson> lessonRepository)
    {
        _lessonRepository = lessonRepository; 
    }
    
    // ---------------------------------------------------
    
    // GET ALL:
    public async Task<IEnumerable<Lesson>> GetAllAsync()
    {
        return await _lessonRepository.GetAllAsync();
    }

    
    // GET BY ID:
    public async Task<Lesson> GetByIdAsync(int id)
    {
        return await _lessonRepository.GetByIdAsync(id);
    }

    
    // CREATE:
    public async Task<Lesson> CreateAsync(Lesson lesson)
    {
        return await _lessonRepository.CreateAsync(lesson);
    }

    
    // UPDATE:
    public async Task<bool> UpdateAsync(Lesson lesson)
    {
        var exists = await _lessonRepository.GetByIdAsync(lesson.Id);

        if (exists == null)
            return false;

        await _lessonRepository.UpdateAsync(lesson);
        return true;
    }

    
    // DELETE:
    public async Task<bool> DeleteAsync(int id)
    {
        var toDelete = await _lessonRepository.GetByIdAsync(id);

        if (toDelete == null)
            return false;

        await _lessonRepository.DeleteAsync(toDelete);
        return true;
    }
}