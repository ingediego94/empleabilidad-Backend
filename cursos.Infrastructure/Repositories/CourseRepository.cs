using cursos.Domain.Entities;
using cursos.Domain.Interfaces;
using cursos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace cursos.Infrastructure.Repositories;

public class CourseRepository : IGeneralRepository<Course>
{
    private readonly AppDbContext _context;

    public CourseRepository(AppDbContext context)
    {
        _context = context;
    }
    // --------------------------------------------
    
    // GET ALL:
    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        return await _context.Courses
            .Where(c => !c.IsDeleted)
            .ToListAsync();
    }

    
    // GET BY ID:
    public async Task<Course?> GetByIdAsync(int id)
    {
        return await _context.Courses
            .Include(c => c.Lessons)
            .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
    }

    
    // CREATE:
    public async Task<Course> CreateAsync(Course course)
    {
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
        return course;
    }

    
    // UPDATE:
    public async Task<Course?> UpdateAsync(Course course)
    {
        _context.Courses.Update(course);
        await _context.SaveChangesAsync();
        return course;
    }

    
    // DELETE:
    public async Task<bool> DeleteAsync(Course course)
    {
        course.IsDeleted = true;
        course.UpdatedAt = DateTime.UtcNow;

        _context.Courses.Update(course);
        await _context.SaveChangesAsync();
        return true;
    }
}