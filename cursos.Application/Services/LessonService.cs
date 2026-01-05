using AutoMapper;
using cursos.Application.DTOs;
using cursos.Application.Interfaces;
using cursos.Domain.Entities;
using cursos.Domain.Interfaces;

namespace cursos.Application.Services;

public class LessonService : ILessonService
{
    private readonly IGeneralRepository<Lesson> _lessonRepository;
    private readonly IMapper _mapper;

    public LessonService(IGeneralRepository<Lesson> lessonRepository, IMapper mapper)
    {
        _lessonRepository = lessonRepository;
        _mapper = mapper;
    }
    
    // ---------------------------------------------------
    
    // GET ALL:
    public async Task<IEnumerable<Lesson>> GetAllAsync()
    {
        return await _lessonRepository.GetAllAsync();
    }

    
    // GET BY ID:
    public async Task<Lesson?> GetByIdAsync(int id)
    {
        return await _lessonRepository.GetByIdAsync(id);
    }

    
    // CREATE:
    public async Task<ResponseLessonDto> CreateAsync(LessonCreateDto dto)
    {
        // 1. Validación de regla de negocio
        var lessons = await _lessonRepository.GetAllAsync();

        if (lessons.Any(l =>
                l.CourseId == dto.CourseId &&
                l.Order == dto.Order &&
                !l.IsDeleted))
        {
            throw new Exception("Lesson order already exists in this course");
        }

        // 2. Crear entidad
        var lesson = _mapper.Map<Lesson>(dto);
        lesson.CreatedAt = DateTime.UtcNow;
        lesson.UpdatedAt = DateTime.UtcNow;

        // 3. Persistir
        await _lessonRepository.CreateAsync(lesson);

        // 4. Responder
        return _mapper.Map<ResponseLessonDto>(lesson);
    }


    
    // UPDATE:
    public async Task<ResponseLessonDto?> UpdateAsync(int id, LessonUpdateDto dto)
    {
        var lesson = await _lessonRepository.GetByIdAsync(id);
        if (lesson == null || lesson.IsDeleted)
            return null;

        lesson.Title = dto.Title;
        lesson.Order = dto.Order;
        lesson.UpdatedAt = DateTime.UtcNow;

        await _lessonRepository.UpdateAsync(lesson);
        return _mapper.Map<ResponseLessonDto>(lesson);
    }

    
    // DELETE:
    public async Task<bool> DeleteAsync(int id)
    {
        var lesson = await _lessonRepository.GetByIdAsync(id);
        if (lesson == null)
            return false;

        await _lessonRepository.DeleteAsync(lesson);
        return true;
    }
}