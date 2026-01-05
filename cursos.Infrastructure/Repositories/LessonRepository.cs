using cursos.Domain.Entities;
using cursos.Domain.Interfaces;
using cursos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace cursos.Infrastructure.Repositories;

public class LessonRepository : IGeneralRepository<Lesson>
{
    private readonly AppDbContext _context;

    public LessonRepository(AppDbContext context)
    {
        _context = context;
    }
    // --------------------------------------------
    
    // GET ALL:
    public async Task<IEnumerable<Lesson>> GetAllAsync()
    {
        return await _context.Lessons
            .Where(l => !l.IsDeleted)
            .ToListAsync();
    }

    
    // GET BY ID:
    public async Task<Lesson?> GetByIdAsync(int id)
    {
        return await _context.Lessons
            .FirstOrDefaultAsync(l => l.Id == id && !l.IsDeleted);
    }

    
    // CREATE:
    public async Task<Lesson> CreateAsync(Lesson lesson)
    {
        _context.Lessons.Add(lesson);
        await _context.SaveChangesAsync();
        return lesson;
    }

    
    // UPDATE:
    public async Task<Lesson?> UpdateAsync(Lesson lesson)
    {
        _context.Lessons.Update(lesson);
        await _context.SaveChangesAsync();
        return lesson;
    }

    
    // DELETE:
    public async Task<bool> DeleteAsync(Lesson lesson)
    {
        lesson.IsDeleted = true;
        lesson.UpdatedAt = DateTime.UtcNow;

        _context.Lessons.Update(lesson);
        await _context.SaveChangesAsync();
        return true;
    }
}
