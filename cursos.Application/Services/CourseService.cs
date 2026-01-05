using AutoMapper;
using cursos.Application.DTOs;
using cursos.Application.Interfaces;
using cursos.Domain.Entities;
using cursos.Domain.Enum;
using cursos.Domain.Interfaces;

public class CourseService : ICourseService
{
    private readonly IGeneralRepository<Course> _repo;
    private readonly IMapper _mapper;

    public CourseService(
        IGeneralRepository<Course> repo,
        IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    
    
    
    public async Task<ResponseCourseDto> CreateAsync(CourseCreateDto dto)
    {
        var course = _mapper.Map<Course>(dto);
        course.Status = Status.Draft;

        await _repo.CreateAsync(course);
        return _mapper.Map<ResponseCourseDto>(course);
    }

    
    
    
    public async Task<IEnumerable<ResponseCourseDto>> GetAllAsync()
    {
        var courses = await _repo.GetAllAsync();
        return _mapper.Map<IEnumerable<ResponseCourseDto>>(courses);
    }

    
    
    public async Task<ResponseCourseDto> GetByIdAsync(int id)
    {
        var course = await _repo.GetByIdAsync(id)
            ?? throw new Exception("Course not found");

        if (course.IsDeleted)
            throw new Exception("Course is deleted");

        return _mapper.Map<ResponseCourseDto>(course);
    }

    
    
    
    public async Task<ResponseCourseDto> UpdateAsync(int id, CourseUpdateDto dto)
    {
        var course = await _repo.GetByIdAsync(id)
            ?? throw new Exception("Course not found");

        if (course.IsDeleted)
            throw new Exception("Course is deleted");

        course.Title = dto.Title;
        course.UpdatedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(course);
        return _mapper.Map<ResponseCourseDto>(course);
    }
    
    
    

    public async Task<bool> DeleteAsync(int id)
    {
        var course = await _repo.GetByIdAsync(id)
            ?? throw new Exception("Course not found");

        return await _repo.DeleteAsync(course);
    }

    
    
    
    public async Task<bool> PublishAsync(int id)
    {
        var course = await _repo.GetByIdAsync(id)
                     ?? throw new Exception("Course not found");

        if (course.IsDeleted)
            throw new Exception("Course is deleted");

        var activeLessons = course.Lessons
            .Count(l => !l.IsDeleted);

        if (activeLessons == 0)
            throw new Exception("Cannot publish course without lessons");

        course.Status = Status.Published;
        course.UpdatedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(course);
        return true;
    }

    
    

    // SUMMARY
    public async Task<CourseSummaryDto> GetSummaryAsync(int id)
    {
        var course = await _repo.GetByIdAsync(id)
            ?? throw new Exception("Course not found");

        return new CourseSummaryDto
        {
            Id = course.Id,
            Title = course.Title,
            Status = course.Status,
            TotalLessons = course.Lessons.Count(l => !l.IsDeleted),
            LastUpdate = course.UpdatedAt
        };
    }

    public async Task<PagedResult<ResponseCourseDto>> SearchAsync(
        string? q,
        Status? status,
        int page,
        int pageSize)
    {
        var courses = await _repo.GetAllAsync();

        // Solo activos
        var query = courses.Where(c => !c.IsDeleted);

        if (!string.IsNullOrWhiteSpace(q))
            query = query.Where(c =>
                c.Title.ToLower().Contains(q.ToLower()));

        if (status.HasValue)
            query = query.Where(c => c.Status == status.Value);

        var totalItems = query.Count();

        var items = query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(c => _mapper.Map<ResponseCourseDto>(c))
            .ToList();

        return new PagedResult<ResponseCourseDto>
        {
            Items = items,
            TotalItems = totalItems,
            Page = page,
            PageSize = pageSize
        };
    }



    public async Task<bool> UnpublishAsync(int id)
    {
        var course = await _repo.GetByIdAsync(id)
                     ?? throw new Exception("Course not found");

        if (course.IsDeleted)
            throw new Exception("Course is deleted");

        course.Status = Status.Draft;
        course.UpdatedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(course);
        return true;
    }

}
